using UnityEngine;
using System.Collections;

public class Dirt : MonoBehaviour
{
	float nitrogen;
	
	GameObject plant;
	
	float water;
	
	public GameObject GetPlant()
	{
		return plant;
	}
	
	public void SetPlant(GameObject plant)
	{
		this.plant = plant;

		Vector3 plantPosition = transform.position;
		plantPosition.z = -2.0f;
		plant.transform.position = plantPosition;
	}

	void Start()
	{
		nitrogen = 10.0f;
		water = 10.0f;
	}
	
	void Update()
	{
		nitrogen -= 0.01f * Time.deltaTime;
		water -= 0.01f * Time.deltaTime;
	}
}
