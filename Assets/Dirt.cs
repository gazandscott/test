using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dirt : MonoBehaviour
{	
	Dictionary<Nutrient, float> nutrients;
	
	GameObject plantObject;
	
	public float Consume(Nutrient nutrient, float quantity)
	{
		nutrients[nutrient] = nutrients[nutrient] - quantity;
		
		return quantity;
	}
	
	public Dictionary<Nutrient, float> GetNutrients()
	{
		return nutrients;
	}
	
	public GameObject GetPlantObject()
	{
		return plantObject;
	}
	
	public void SetPlantObject(GameObject plantObject)
	{
		this.plantObject = plantObject;

		Vector3 plantPosition = transform.position;
		plantPosition.z = -2.0f;
		plantObject.transform.position = plantPosition;
	}

	void Start()
	{
		nutrients = new Dictionary<Nutrient, float>();
		
		nutrients[Nutrient.H2O] = 10.0f;
		nutrients[Nutrient.N] = 10.0f;
	}
	
	void Update()
	{
		nutrients[Nutrient.H2O] = nutrients[Nutrient.H2O] - (0.01f * Time.deltaTime);
		nutrients[Nutrient.N] = nutrients[Nutrient.N] - (0.01f * Time.deltaTime);
	}
}
