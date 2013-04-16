using UnityEngine;
using System.Collections;

public class Planter : MonoBehaviour
{
	public Material cloverMaterial;
	
	GameObject dirtObject;
	
	public Material flowerMaterial;
	
	Vector2 menuLocation;
	
	public Material vegetableMaterial;
	
    void OnGUI()
	{		
		if (dirtObject != null)
		{
			Player player = (Player) GetComponent("Player");
			GUI.Box(new Rect(menuLocation.x, menuLocation.y, 130, 120), "Planter");
		
			if(GUI.Button(new Rect(menuLocation.x + 10, menuLocation.y + 30, 110, 20), "Clover ($50)"))
			{
				if (player.Spend(50.0f))
				{
					Plant(PlantFactory.createClover(cloverMaterial));
					return;
				}
			}
		
			if(GUI.Button(new Rect(menuLocation.x + 10, menuLocation.y + 60, 110, 20), "Flower ($30)"))
			{
				if (player.Spend(30.0f))
				{
					Plant(PlantFactory.createFlower(flowerMaterial));
					return;
				}
			}
		
			if(GUI.Button(new Rect(menuLocation.x + 10, menuLocation.y + 90, 110, 20), "Vegetable ($10)"))
			{
				if (player.Spend(10.0f))
				{
					Plant(PlantFactory.createVegetable(vegetableMaterial));
					return;
				}
			}
		}
		
		if (Event.current.type == EventType.mouseUp && Event.current.button == 0)
		{
			SelectDirt();
		}
    }
	
	void Plant(GameObject plantObject)
	{
		Dirt dirt = (Dirt) dirtObject.GetComponent("Dirt");
		Plant plant = (Plant) plantObject.GetComponent("Plant");
		
		dirt.SetPlantObject(plantObject);
		plant.SetDirtObject(dirtObject);
		
		dirtObject = null;
	}
	
	void SelectDirt()
	{
		GameObject clickedObject = Utils.GetGameObjectAtMousePosition();
		if (clickedObject != null)
		{
			if (clickedObject.name.StartsWith("Dirt"))
			{
				Dirt dirt = (Dirt) clickedObject.GetComponent("Dirt");
				if (dirt.GetPlantObject() == null)
				{
					dirtObject = clickedObject;
					menuLocation = Event.current.mousePosition;
				}
			}
		}
	}

	void Start()
	{
		dirtObject = null;
	}

	void Update()
	{
	}
}
				