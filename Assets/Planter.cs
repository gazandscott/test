using UnityEngine;
using System.Collections;

public class Planter : MonoBehaviour
{	
	public void Plant(GameObject plantObject)
	{
		GameObject dirtObject = GetComponent<UserInterface>().SelectedDirtObject;
		Dirt dirt = dirtObject.GetComponent<Dirt>();
		Plant plant = plantObject.GetComponent<Plant>();
		
		dirt.PlantObject = plantObject;
		plant.DirtObject = dirtObject;
		
		dirtObject = null;
	}
	
	public void Plant(Species species)
	{
		PlantFactory plantFactory = GetComponent<PlantFactory>();
		Player player = GetComponent<Player>();
		
		if (species == Species.CLOVER)
		{
			if (player.Spend(50.0f))
			{
				Plant(plantFactory.CreateClover());
				return;
			}
		}
		else if (species == Species.FLOWER)
		{
			if (player.Spend(30.0f))
			{
				Plant(plantFactory.CreateFlower());
				return;
			}
		}
		else if (species == Species.VEGETABLE)
		{
			if (player.Spend(10.0f))
			{
				Plant(plantFactory.CreateVegetable());
				return;
			}
		}
	}

	void Start()
	{
	}

	void Update()
	{
	}
}
