using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dirt : MonoBehaviour
{
	public static float EXTENT = 1.0f;

	Dictionary<Nutrient, float> nutrients;
	
	GameObject plantObject;
	
	public float Consume(Nutrient nutrient, float quantity)
	{		
		nutrients[nutrient] = nutrients[nutrient] - quantity;
		
		if (nutrients[nutrient] < 0.0f)
		{
			float unavailableQuantity = -nutrients[nutrient];
			nutrients[nutrient] = 0.0f;
			
			return quantity - unavailableQuantity;
		}
		
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
	
	public float Provide(Nutrient nutrient, float quantity)
	{
		return nutrients[nutrient] = nutrients[nutrient] + quantity;
	}

	void Start()
	{
		nutrients = new Dictionary<Nutrient, float>();
		
		nutrients[Nutrient.H2O] = 1.0f;
		nutrients[Nutrient.N] = 1.0f;
	}
	
	void Update()
	{
		List<Nutrient> keys = new List<Nutrient>(nutrients.Keys);
		foreach (Nutrient nutrient in keys)
		{
			nutrients[nutrient] -= 0.01f * Time.deltaTime;
			if (nutrients[nutrient] < 0.0f)
			{
				nutrients[nutrient] = 0.0f;
			}
		}
	}
}
