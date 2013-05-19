using UnityEngine;
using System.Collections;

public class Repotter : MonoBehaviour
{
	bool repotting;
	
	GameObject toBeRepotted;
	
	void Awake()
	{
		repotting = false;	
	}
	
	public bool IsRepotting()
	{
		return repotting;
	}
	
	public void Repot()
	{
		Player player = GetComponent<Player>();
		GameObject dirtObject = GetComponent<UserInterface>().SelectedDirtObject;
		
		if(repotting && dirtObject.GetComponent<Dirt>().PlantObject == null)
		{
			if (player.Spend(1.0f))
			{
				repotting = false;
				
				// Point plant to new dirt ref
				toBeRepotted.GetComponent<Plant>().DirtObject = dirtObject;
			
				// Point new dirt to plant ref
				dirtObject.GetComponent<Dirt>().PlantObject = toBeRepotted;
			}
		}
	}
	
	void Start()
	{
	}
	
	public void Unpot()
	{
		Player player = GetComponent<Player>();
		GameObject dirtObject = GetComponent<UserInterface>().SelectedDirtObject;
		
		if (player.Spend(10.0f))
		{
			repotting = true;
			toBeRepotted = dirtObject.GetComponent<Dirt>().PlantObject;
			
			// Clear current plant referrence to dirt
			dirtObject.GetComponent<Dirt>().PlantObject = null;
		}
	}
	
	void Update()
	{
	}
}
