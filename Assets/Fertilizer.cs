using UnityEngine;
using System.Collections;

public class Fertilizer : MonoBehaviour
{
	public void Fertilize()
	{
		Player player = GetComponent<Player>();
		GameObject dirtObject = GetComponent<UserInterface>().GetSelectedDirtObject();
		
		if (player.Spend(10.0f))
		{
			Dirt dirt = dirtObject.GetComponent<Dirt>();
			dirt.Provide(Nutrient.N, 100);
			
			// Particles for watering schtuff
			GameObject fertilizer = (GameObject) Instantiate(GameObject.Find("Fertiliser"));
			Vector3 fertilizerPosition = dirtObject.transform.position;
			fertilizerPosition.z = -4.0f;
			fertilizerPosition.y += Dirt.EXTENT * 0.5f;
			fertilizer.transform.position = fertilizerPosition;
		}
	}
	
	void Start()
	{
	
	}
	
	void Update()
	{
	
	}
}
