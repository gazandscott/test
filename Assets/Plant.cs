using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plant : MonoBehaviour
{	
	GameObject dirtObject;
	
	float growthRate;
	
	float lastGrowTime;
	
	float maxSize;
	
	Dictionary<Nutrient, int> minimumNutrients;
	
	Dictionary<Nutrient, int> optimumNutrients;
	
	Dictionary<Nutrient, int> providedNutrients;
	
	Dictionary<Nutrient, int> providedNutrientRanges;
	
	float size;
	
	float Consume()
	{
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
		
		return growthFactor;
	}
	
	public GameObject GetDirtObject()
	{
		return dirtObject;
	}
	
	void Grow(float growthFactor)
	{
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
	
	public void Init(Dictionary<Nutrient, int> minimumNutrients, Dictionary<Nutrient, int> optimumNutrients)
	{
		this.minimumNutrients = minimumNutrients;
		this.optimumNutrients = optimumNutrients;
		this.providedNutrients = new Dictionary<Nutrient, int>();
		this.providedNutrientRanges = new Dictionary<Nutrient, int>();
	}
	
	public void Init(Dictionary<Nutrient, int> minimumNutrients, Dictionary<Nutrient, int> optimumNutrients,
		Dictionary<Nutrient, int> providedNutrients, Dictionary<Nutrient, int> providedNutrientRanges)
	{
		this.minimumNutrients = minimumNutrients;
		this.optimumNutrients = optimumNutrients;
		this.providedNutrients = providedNutrients;
		this.providedNutrientRanges = providedNutrientRanges;
	}
	
	void Provide()
	{
		Dirt dirt = (Dirt) dirtObject.GetComponent("Dirt");
		
		foreach (Nutrient nutrient in providedNutrients.Keys)
		{
			Provide(dirt, nutrient, providedNutrients[nutrient], providedNutrientRanges[nutrient]);
		}
	}
	
	void Provide(Dirt dirt, Nutrient nutrient, int quantity, int range)
	{
		dirt.Provide(nutrient, quantity);
		
		if (range > 1)
		{
			foreach (GameObject adjacentDirtObject in dirt.GetAdjacentDirtObjects())
			{
				Dirt adjacentDirt = (Dirt) adjacentDirtObject.GetComponent("Dirt");
				Provide(adjacentDirt, nutrient, quantity, range - 1);
			}
		}
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
			
			float growthFactor = Consume();
			
			Provide();
			
			Grow(growthFactor);
		}
	}
}
