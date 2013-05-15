using UnityEngine;
using System.Collections;

public class Planter : MonoBehaviour
{
	public Material cloverMaterial;
	
	public Material flowerMaterial;
	
	public Material vegetableMaterial;
	
	void Plant(GameObject plantObject)
	{
		GameObject dirtObject = GetComponent<UserInterface>().GetSelectedDirtObject();
		Dirt dirt = dirtObject.GetComponent<Dirt>();
		Plant plant = plantObject.GetComponent<Plant>();
		
		dirt.SetPlantObject(plantObject);
		plant.SetDirtObject(dirtObject);
		
		dirtObject = null;
	}
	
	public void Plant(PlantType plantType)
	{
		Player player = GetComponent<Player>();
		
		if (plantType == PlantType.CLOVER)
		{
			if (player.Spend(50.0f))
			{
				Plant(PlantFactory.createClover(cloverMaterial));
				return;
			}
		}
		else if (plantType == PlantType.FLOWER)
		{
			if (player.Spend(30.0f))
			{
				Plant(PlantFactory.createFlower(flowerMaterial));
				return;
			}
		}
		else if (plantType == PlantType.VEGETABLE)
		{
			if (player.Spend(10.0f))
			{
				Plant(PlantFactory.createVegetable(vegetableMaterial));
				return;
			}
			}
			// This is for repotting
			/*if(GUI.Button (new Rect(menuLocation.x + 10, menuLocation.y + 120, 110, 20), "Un-pot Plant ($10)"))
			{					
				if(player.Spend(10.0f))
				{
					RePotting = dirtObject.GetComponent<Dirt>().GetPlantObject();
					// Clear current plant referrence to dirt
					dirtObject.GetComponent<Dirt>().SetPlantObject(null);
					
					bRepot = true;	
				}
			}
			// Repotting
			if(GUI.Button (new Rect(menuLocation.x + 10, menuLocation.y + 150, 110, 20), "Re-pot Plant ($1)"))
			{	
				
				if(player.Spend(1.0f))
				{
					if(dirtObject.GetComponent<Dirt>().GetPlantObject() == null)
					{
					// Point plant to new dirt ref
					RePotting.GetComponent<Plant>().SetDirtObject(dirtObject);
				
					// Point new dirt to plant ref
					dirtObject.GetComponent<Dirt>().SetPlantObject(RePotting);
					}
				}*/
				
		}
	}

	void Start()
	{
	}

	void Update()
	{
	}
}
