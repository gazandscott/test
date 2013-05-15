using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{
	GameObject dirtObject;
	
    void OnGUI()
	{
        if (Event.current.type == EventType.mouseUp && Event.current.button == 0)
		{
			GameObject clickedObject = Utils.GetGameObjectAtMousePosition();
			if (clickedObject != null)
			{
				if (clickedObject.name.StartsWith("Dirt"))
				{
					dirtObject = clickedObject;
				}
				
				if (clickedObject.name.StartsWith("Plant"))
				{
					Plant plant = (Plant) clickedObject.GetComponent("Plant");
					dirtObject = plant.GetDirtObject();
				}
			}
		}
		
		int y = 250;
		
		GUI.Box(new Rect(0, y, 100, 105), "", Utils.GetGUIStyle());
		
		Player player = (Player) GetComponent("Player");
		GUI.Label(new Rect(10, y + 10, 80, 20), "Money: $" + player.GetMoney());
	
		if (dirtObject != null)
		{
			Dirt dirt = (Dirt) dirtObject.GetComponent("Dirt");
			GUI.Label(new Rect(10, y + 35, 80, 20), "Selected Dirt:");
			GUI.Label(new Rect(10, y + 55, 80, 20), "H2O: " + dirt.GetNutrients()[Nutrient.H2O]);
			GUI.Label(new Rect(10, y + 75, 80, 20), "N: " + dirt.GetNutrients()[Nutrient.N]);
		}
    }
	
	void Start ()
	{
	}
	
	void Update ()
	{
	}
}
