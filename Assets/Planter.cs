using UnityEngine;
using System.Collections;

public class Planter : MonoBehaviour
{
	public Material cloverMaterial;
	
	public Material flowerMaterial;
	
	public Material vegetableMaterial;
	
	void Plant(GameObject plantObject)
	{
		GameObject dirtObject = GetComponent<UserInterface>().SelectedDirtObject;
		Dirt dirt = dirtObject.GetComponent<Dirt>();
		Plant plant = plantObject.GetComponent<Plant>();
		
		dirt.PlantObject = plantObject;
		plant.DirtObject = dirtObject;
		
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
	}

	void Start()
	{
	}

	void Update()
	{
	}
}
