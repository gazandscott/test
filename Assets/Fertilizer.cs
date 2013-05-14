using UnityEngine;
using System.Collections;

public class Fertilizer : MonoBehaviour
{
	void OnGUI()
	{
		if(Event.current.type == EventType.mouseUp && Event.current.button == 2)
		{
			GameObject clickedObject = Utils.GetGameObjectAtMousePosition();
			
			if(clickedObject != null && clickedObject.name.StartsWith("Dirt"))
			{
				GameObject theGame = GameObject.Find("The Game");
				Player player = (Player) theGame.GetComponent("Player");
				if (player.Spend(10.0f))
				{
					Dirt dirt = (Dirt) clickedObject.GetComponent("Dirt");
					dirt.Provide(Nutrient.N, 100);
					
					// Particles for watering schtuff
					GameObject fertilizer = (GameObject) Instantiate(GameObject.Find("Fertiliser"));
					Vector3 fertilizerPosition = clickedObject.transform.position;
					fertilizerPosition.z = -4.0f;
					fertilizerPosition.y += Dirt.EXTENT * 0.5f;
					fertilizer.transform.position = fertilizerPosition;
				}
			}	
			else if(clickedObject != null && clickedObject.name.StartsWith("Plant"))
			{
				GameObject theGame = GameObject.Find("The Game");
				
				Player player = (Player) theGame.GetComponent("Player");
				
				if (player.Spend(10.0f))
				{
					Dirt dirt = clickedObject.GetComponent<Plant>().GetDirtObject().GetComponent<Dirt>();
					
					dirt.Provide(Nutrient.N, 100);
					
					// Particles for watering schtuff
					GameObject fertilizer = (GameObject) Instantiate(GameObject.Find("Fertiliser"));
					Vector3 fertilizerPosition = clickedObject.GetComponent<Plant>().GetDirtObject().transform.position;
					fertilizerPosition.z = -4.0f;
					fertilizerPosition.y += Dirt.EXTENT * 0.5f;
					fertilizer.transform.position = fertilizerPosition;
				}
			}
		}

		
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
