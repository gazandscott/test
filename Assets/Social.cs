using UnityEngine;
using System.Collections;

public class Social : MonoBehaviour
{
	bool connected;
	bool host;
	float hostPollTime;
	
	void Awake()
	{
		MasterServer.RequestHostList("GardeningGame");
		Debug.Log("Getting host list...");
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

	void Start()
	{
		connected = false;
		host = false;
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
