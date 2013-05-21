using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Social : MonoBehaviour
{
	bool connected;
	
	bool host;
	
	float hostPollTime;
	
	int tradePartnerIndex;
		
	int theirMoneyTrade;
		
	Dictionary<Species, int> theirPlantTrades;
	
	bool theirTradeAcceptance;
		
	int yourMoneyTrade;
	
	Dictionary<Species, int> yourPlantTrades;
	
	bool yourTradeAcceptance;
	
	void Awake()
	{
		MasterServer.RequestHostList("GardeningGame");
		Debug.Log("Getting host list...");
	}
	
	void DisplayMoneyTrade(bool yours, int x, int y)
	{
		GUI.Label(new Rect(x, y, 80, 20), "Money");
		
		if (yours)
		{
			int newTradeCount = Convert.ToInt32(GUI.TextField(new Rect(x + 100, y, 80, 20), yourMoneyTrade.ToString()));
			if (newTradeCount != yourMoneyTrade)
			{
				yourMoneyTrade = newTradeCount;
				theirTradeAcceptance = false;
				yourTradeAcceptance = false;
				networkView.RPC("OnTradeUpdateMoney", Network.connections[tradePartnerIndex], newTradeCount);
			}
		}
		else
		{
			GUI.Label(new Rect(x + 100, y, 80, 20), theirMoneyTrade.ToString());
		}
	}
	
	void DisplayPlantTrade(Species species, bool yours, int x, int y)
	{
		GUI.Label(new Rect(x, y, 80, 20), species.GetName());
		
		if (yours)
		{
			int newTradeCount = Convert.ToInt32(GUI.TextField(new Rect(x + 100, y, 80, 20), yourPlantTrades[species].ToString()));
			if (newTradeCount != yourPlantTrades[species])
			{
				yourPlantTrades[species] = newTradeCount;
				theirTradeAcceptance = false;
				yourTradeAcceptance = false;
				networkView.RPC("OnTradeUpdatePlant", Network.connections[tradePartnerIndex], (int) species, newTradeCount);
			}
		}
		else
		{
			GUI.Label(new Rect(x + 100, y, 80, 20), theirPlantTrades[species].ToString());
		}
	}
	
	void OnApplicationQuit()
	{
		if (host)
		{
			MasterServer.UnregisterHost();
		}
		
		if (connected)
		{
			Network.Disconnect();
		}
	}
	
	void OnGUI()
	{
		GUI.Box(new Rect(Screen.width - 200, 0, 200, 105), "Remote Players", Utils.GetGUIStyle());
		
		int y = 10;
		for (int index = 0; index < Network.connections.GetLength(0); index++)
		{
			GUI.Label(new Rect(Screen.width - 190, y, 80, 20), Network.connections[index].ipAddress);
			if (GUI.Button(new Rect(Screen.width - 90, y, 80, 20), "Trade"))
			{
				tradePartnerIndex = index;
				ResetTrade();
				networkView.RPC("OnTradeInitiate", Network.connections[tradePartnerIndex]);
			}
			
			y += 30;
		}
		
		if (tradePartnerIndex != -1)
		{
			int x = Screen.width / 2 - 200;
			y = Screen.height / 2 - 200;
			GUI.Box(new Rect(x, y, 400, 400), "Trade", Utils.GetGUIStyle());
			
			x += 10;
			y += 10;
			
			GUI.Label(new Rect(x, y, 80, 20), "Yours");
			y += 30;
			DisplayMoneyTrade(true, x, y);
			y += 30;
			List<Species> yourSpecies = new List<Species>(yourPlantTrades.Keys);
			foreach (Species species in yourSpecies)
			{
				DisplayPlantTrade(species, true, x, y);
				y += 30;
			}
			if (yourTradeAcceptance)
			{
				GUI.Label(new Rect(x, y, 80, 20), "Accepted");
			}
			else
			{
				if (GUI.Button(new Rect(x, y, 80, 20), "Accept"))
				{
					yourTradeAcceptance = true;
					networkView.RPC("OnTradeAccept", Network.connections[tradePartnerIndex]);
					if (theirTradeAcceptance)
					{
						PerformTrade();
					}
				}
			}
			
			x += 200;
			y = Screen.height / 2 - 200 + 10;
			
			GUI.Label(new Rect(x, y, 80, 20), "Theirs");
			y += 30;
			DisplayMoneyTrade(false, x, y);
			y += 30;
			List<Species> theirSpecies = new List<Species>(theirPlantTrades.Keys);
			foreach (Species species in theirSpecies)
			{
				DisplayPlantTrade(species, false, x, y);
				y += 30;
			}
			if (theirTradeAcceptance)
			{
				GUI.Label(new Rect(x, y, 80, 20), "Accepted");
			}
			else
			{
				GUI.Label(new Rect(x, y, 80, 20), "Not accepted");
			}
		}
	}
	
	[RPC]
	void OnTradeAccept()
	{
		theirTradeAcceptance = true;
		if (yourTradeAcceptance)
		{
			PerformTrade();
		}
	}
	
	[RPC]
	void OnTradeInitiate(NetworkMessageInfo info)
	{
		for (int index = 0; index < Network.connections.GetLength(0); index++)
		{
			if (Network.connections[index].guid == info.sender.guid)
			{
				tradePartnerIndex = index;
				break;
			}
		}
		
		ResetTrade();
	}
	
	[RPC]
	void OnTradeUpdateMoney(int newTradeCount)
	{
		theirMoneyTrade = newTradeCount;
		theirTradeAcceptance = false;
		yourTradeAcceptance = false;
	}
	
	[RPC]
	void OnTradeUpdatePlant(int species, int newTradeCount)
	{
		theirPlantTrades[(Species) species] = newTradeCount;
		theirTradeAcceptance = false;
		yourTradeAcceptance = false;
	}
	
	void PerformTrade()
	{
		PlantFactory plantFactory = GetComponent<PlantFactory>();
		Player player = GetComponent<Player>();
		
		player.Spend(yourMoneyTrade);
		player.Earn(theirMoneyTrade);
		
		foreach (KeyValuePair<Species, int> plantTrade in theirPlantTrades)
		{
			for (int index = 0; index < plantTrade.Value; index++)
			{
				player.UnplantedPlants.Add(plantFactory.Create(plantTrade.Key));
			}
		}
		
		tradePartnerIndex = -1;
	}
	
	void ResetTrade()
	{
		theirMoneyTrade = 0;
		theirPlantTrades.Clear();
		theirPlantTrades.Add(Species.CLOVER, 0);
		theirPlantTrades.Add(Species.MARIGOLD, 0);
		theirPlantTrades.Add(Species.TOMATO, 0);
		theirTradeAcceptance = false;
		yourMoneyTrade = 0;
		yourPlantTrades.Clear();
		yourPlantTrades.Add(Species.CLOVER, 0);
		yourPlantTrades.Add(Species.MARIGOLD, 0);
		yourPlantTrades.Add(Species.TOMATO, 0);
		yourTradeAcceptance = false;
	}
	
	void Start()
	{
		connected = false;
		host = false;
		theirPlantTrades = new Dictionary<Species, int>();
		tradePartnerIndex = -1;
		yourMoneyTrade = 0;
		yourPlantTrades = new Dictionary<Species, int>();
	}

	void Update()
	{
		if (!connected)
		{
			hostPollTime += Time.deltaTime;
			if (hostPollTime > 1.0f)
			{
				// Use NAT punchthrough if no public IP present
				Network.InitializeServer(32, 25002, !Network.HavePublicAddress());
				MasterServer.RegisterHost("GardeningGame", "TheGame");
				connected = true;
				host = true;
				Debug.Log("Hosting game...");
			}
			
			HostData[] hosts = MasterServer.PollHostList();
			if (hosts.Length != 0)
			{
				// Connect to HostData struct, internally the correct method is used (GUID when using NAT).
				if (Network.Connect(hosts[0]) == NetworkConnectionError.NoError)
				{
					connected = true;
					Debug.Log("Joining game...");
				}
			}
		}
	}
}
