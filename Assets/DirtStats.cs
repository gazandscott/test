using UnityEngine;
using System.Collections;

public class DirtStats : MonoBehaviour
{
	GameObject dirtObject;
	
	float lastStatDisplayTime;
	
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
    }
	
	void Start ()
	{
		lastStatDisplayTime = 0.0f;
	}
	
	void Update ()
	{
		if (Time.timeSinceLevelLoad - lastStatDisplayTime > 1.0f)
		{
			lastStatDisplayTime = Time.timeSinceLevelLoad;
			GUIText text = (GUIText) GameObject.Find("DirtStats").GetComponent("GUIText");
			
			if (dirtObject == null)
			{
				text.text = "Dirt Stats!";
				return;
			}
		
			Dirt dirt = (Dirt) dirtObject.GetComponent("Dirt");
			text.text = "Dirt Stats!\nH2O: " + dirt.GetNutrients()[Nutrient.H2O] + "\nN: " + dirt.GetNutrients()[Nutrient.N];
		}
	}
}
