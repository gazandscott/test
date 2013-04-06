using UnityEngine;
using System.Collections;

public class WateringCan : MonoBehaviour
{
	GameObject clickedGameObj;
	
	bool clickedObjAquired;
	
	Dirt dirt;
	
	void OnGUI()
	{		
		if(Event.current.type == EventType.mouseUp && Event.current.button == 1)
		{
			clickedGameObj = Utils.GetGameObjectAtMousePosition();
			
			if(clickedGameObj != null && clickedGameObj.name.StartsWith("Dirt"))
			{
				// Particles for watering schtuff
				GameObject WaterDroplets = (GameObject) Instantiate(GameObject.Find("Water Drops"));
				Vector3 waterPosition = clickedGameObj.transform.position;
				waterPosition.z = -4.0f;
				WaterDroplets.transform.position = waterPosition;
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
