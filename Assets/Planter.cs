using UnityEngine;
using System.Collections;

public class Planter : MonoBehaviour
{
	public Material cloverMaterial;
	
	GameObject dirtObject;
	
	public Material flowerMaterial;
	
	Vector2 menuLocation;
	
	public Material vegetableMaterial;
	
	GameObject RePotting;
	
	public bool bRepot;
	
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
			// This is for repotting
			if(GUI.Button (new Rect(menuLocation.x + 10, menuLocation.y + 120, 110, 20), "Un-pot Plant ($10)"))
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
				if (dirt.IsPlantable() && dirt.GetPlantObject() == null)
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
				