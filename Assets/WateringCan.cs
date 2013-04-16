using UnityEngine;
using System.Collections;

public class WateringCan : MonoBehaviour
{	
	Dirt dirt;
	
	void OnGUI()
	{		
		if(Event.current.type == EventType.mouseUp && Event.current.button == 1)
		{
			GameObject clickedObject = Utils.GetGameObjectAtMousePosition();
			if(clickedObject != null && clickedObject.name.StartsWith("Dirt"))
			{
				GameObject theGame = GameObject.Find("The Game");
				Player player = (Player) theGame.GetComponent("Player");
				if (player.Spend(10.0f))
				{
					Dirt dirt = (Dirt) clickedObject.GetComponent("Dirt");
					dirt.Provide(Nutrient.H2O, 100);
					
					// Particles for watering schtuff
					GameObject WaterDroplets = (GameObject) Instantiate(GameObject.Find("Water Drops"));
					Vector3 waterPosition = clickedObject.transform.position;
					waterPosition.z = -4.0f;
					waterPosition.y += Dirt.EXTENT * 0.5f;
					WaterDroplets.transform.position = waterPosition;
				}
			}	
		}
	}
	
	void Start () 
	{
	}
	
	void Update () 
	{	
	}
}
