using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plant : MonoBehaviour
{	
	GameObject dirtObject;
	
	float growthRate;
	
	float lastGrowTime;
	
	float maxSize;
	
	Dictionary<Nutrient, float> minimumNutrients;
	
	Dictionary<Nutrient, float> optimumNutrients;
	
	float size;
	
	public GameObject GetDirtObject()
	{
		return dirtObject;
	}
	
	public void Init(Dictionary<Nutrient, float> minimumNutrients, Dictionary<Nutrient, float> optimumNutrients)
	{
		this.minimumNutrients = minimumNutrients;
		this.optimumNutrients = optimumNutrients;
	}
	
	public void SetDirtObject(GameObject dirtObject)
	{
		this.dirtObject = dirtObject;
	}

	void Start ()
	{
		growthRate = 0.2f;
		lastGrowTime = Time.timeSinceLevelLoad;
		maxSize = 5.0f;
		size = 1.0f;
		transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
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
			foreach (Nutrient nutrient in optimumNutrients.Keys)
			{
				float consumedQuantity = dirt.Consume(nutrient, optimumNutrients[nutrient]);
				if (consumedQuantity < minimumNutrients[nutrient])
				{
					growthFactor = -1.0f;
					break;
				}
				else if (consumedQuantity < optimumNutrients[nutrient] &&
					consumedQuantity >= minimumNutrients[nutrient])
				{
					growthFactor *= (consumedQuantity - minimumNutrients[nutrient]) / (optimumNutrients[nutrient] / minimumNutrients[nutrient]);
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
