using UnityEngine;
using System.Collections;

public class Potter : MonoBehaviour
{
	void Start()
	{
	}
	
	public void Pot()
	{
		Player player = GetComponent<Player>();
		GameObject dirtObject = GetComponent<UserInterface>().SelectedDirtObject;
		
		if (player.Spend(10.0f))
		{
			GameObject plantObject = dirtObject.GetComponent<Dirt>().PlantObject;
			
			plantObject.GetComponent<Plant>().DirtObject = null;
			dirtObject.GetComponent<Dirt>().PlantObject = null;
			
			plantObject.transform.Translate(-100.0f, 0.0f, 0.0f); // Off screen!
			player.UnplantedPlants.Add(plantObject);
			
		}
	}
	
	void Update()
	{
	}
}
