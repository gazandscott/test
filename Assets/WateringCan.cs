using UnityEngine;
using System.Collections;

public class WateringCan : MonoBehaviour
{	
	public void Water()
	{
		Player player = GetComponent<Player>();
		if (player.Spend(10.0f))
		{
			GameObject dirtObject = GetComponent<UserInterface>().SelectedDirtObject;
			Dirt dirt = dirtObject.GetComponent<Dirt>();
			dirt.Provide(Nutrient.H2O, 100);
			
			// Particles for watering schtuff
			GameObject WaterDroplets = (GameObject) Instantiate(GameObject.Find("Water Drops"));
			Vector3 waterPosition = dirtObject.transform.position;
			waterPosition.z = -4.0f;
			waterPosition.y += Dirt.EXTENT * 0.5f;
			WaterDroplets.transform.position = waterPosition;
		}
	}
	
	void Start () 
	{
	}
	
	void Update () 
	{	
	}
}
