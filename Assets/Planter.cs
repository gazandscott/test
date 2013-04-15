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
			GUI.Box(new Rect(menuLocation.x, menuLocation.y, 100, 120), "Planter");
		
			if(GUI.Button(new Rect(menuLocation.x + 10, menuLocation.y + 30, 80, 20), "Clover"))
			{
				Plant(PlantFactory.createClover(cloverMaterial));
			}
		
			if(GUI.Button(new Rect(menuLocation.x + 10, menuLocation.y + 60, 80, 20), "Flower"))
			{
				Plant(PlantFactory.createClover(flowerMaterial));
			}
		
			if(GUI.Button(new Rect(menuLocation.x + 10, menuLocation.y + 90, 80, 20), "Vegetable"))
			{
				Plant(PlantFactory.createClover(vegetableMaterial));
			}
		}
		
		if (dirtObject == null && Event.current.type == EventType.mouseUp && Event.current.button == 0)
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
				