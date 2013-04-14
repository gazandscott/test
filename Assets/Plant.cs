using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plant : MonoBehaviour
{	
	GameObject dirtObject;
	
	float growthRate;
	
	float lastGrowTime;
	
	float maxSize;
	
	Dictionary<Nutrient, float> nutrientsMinimum;
	
	Dictionary<Nutrient, float> nutrientsOptimum;
	
	float size;
	
	public GameObject GetDirtObject()
	{
		return dirtObject;
	}
	
	public void SetDirtObject(GameObject dirtObject)
	{
		this.dirtObject = dirtObject;
	}

	void Start ()
	{
		growthRate = 0.2f;
		lastGrowTime = 0.0f;
		maxSize = 5.0f;
		nutrientsMinimum = new Dictionary<Nutrient, float>();
		nutrientsOptimum = new Dictionary<Nutrient, float>();
		size = 1.0f;
		transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
		
		nutrientsMinimum[Nutrient.H2O] = 0.03f;
		nutrientsMinimum[Nutrient.N] = 0.01f;
		
		nutrientsOptimum[Nutrient.H2O] = 0.06f;
		nutrientsOptimum[Nutrient.N] = 0.02f;
	}
	
	void Update ()
	{
		// Required because there is an unused instance off to the side! HACK!
		if (dirtObject == null)
		{
			return;	
		}

		if (Time.timeSinceLevelLoad - lastGrowTime > 1.0f / growthRate)
		{
			lastGrowTime = Time.timeSinceLevelLoad;
			Dirt dirt = (Dirt) dirtObject.GetComponent("Dirt");
			
			float growthFactor = 1.0f;
			foreach (Nutrient nutrient in nutrientsOptimum.Keys)
			{
				float consumedQuantity = dirt.Consume(nutrient, nutrientsOptimum[nutrient]);
				if (consumedQuantity < nutrientsMinimum[nutrient])
				{
					growthFactor = -1.0f;
					break;
				}
				else if (consumedQuantity < nutrientsOptimum[nutrient] &&
					consumedQuantity >= nutrientsMinimum[nutrient])
				{
					growthFactor *= (consumedQuantity - nutrientsMinimum[nutrient]) / (nutrientsOptimum[nutrient] / nutrientsMinimum[nutrient]);
				}
			}

			if (size + growthFactor < maxSize)
			{
				size += growthFactor;
				
				if (size <= 0.0f)
				{
					Destroy(gameObject);
				}
				
				Vector3 localScale = new Vector3(0.02f, 0.02f, 0.02f) * size;
				transform.localScale = localScale;
			}
		}
	}
}
