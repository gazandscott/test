using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{
    void OnGUI()
	{
		int y = 250;
		
		GUI.Box(new Rect(0, y, 200, 105), "", Utils.GetGUIStyle());
		
		Player player = GetComponent<Player>();
		GUI.Label(new Rect(10, y + 10, 180, 25), "Money: $" + player.Money);
	
		GameObject dirtObject = GetComponent<UserInterface>().SelectedDirtObject;
		if (dirtObject != null)
		{
			Dirt dirt = (Dirt) dirtObject.GetComponent("Dirt");
			
			GameObject plantObject = dirt.PlantObject;
			string plantName = "None";
			if (plantObject != null)
			{
				plantName = plantObject.GetComponent<Plant>().Species.GetName();
			}
			
			GUI.Label(new Rect(10, y + 35, 180, 25), "Selected Dirt:");
			GUI.Label(new Rect(10, y + 55, 180, 25), "Plant: " + plantName);
			GUI.Label(new Rect(10, y + 75, 180, 25), "H2O: " + dirt.GetNutrients()[Nutrient.H2O]);
			GUI.Label(new Rect(10, y + 95, 180, 25), "N: " + dirt.GetNutrients()[Nutrient.N]);
		}
    }
	
	void Start ()
	{
	}
	
	void Update ()
	{
	}
}
