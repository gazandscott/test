using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{
    void OnGUI()
	{
		int y = 250;
		
		GUI.Box(new Rect(0, y, 100, 105), "", Utils.GetGUIStyle());
		
		Player player = GetComponent<Player>();
		GUI.Label(new Rect(10, y + 10, 80, 20), "Money: $" + player.GetMoney());
	
		GameObject dirtObject = GetComponent<UserInterface>().GetSelectedDirtObject();
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
