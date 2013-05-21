using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plant : MonoBehaviour
{
	float growthRate;
	
	float lastGrowTime;
	
	float lastYieldTime;
	
	float maxSize;
	
	Dictionary<Nutrient, int> minimumNutrients;
	
	Dictionary<Nutrient, int> optimumNutrients;
	
	Dictionary<Nutrient, int> providedNutrients;
	
	Dictionary<Nutrient, int> providedNutrientRanges;
	
	float size;
	
	float yieldFrequency;
	
	float yieldValue;
	
	public GameObject DirtObject
	{
		get;
		set;
	}
	
	public Species Species
	{
		get;
		set;
	}
	
	float Consume()
	{
		Dirt dirt = (Dirt) DirtObject.GetComponent("Dirt");
		
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
	
	void Grow(float growthFactor)
	{
		if (size + growthFactor <= maxSize)
		{
			size += growthFactor;
			
			if (size <= 0.0f)
			{
				Destroy(gameObject);
			}
			
			Vector3 localScale = new Vector3(0.02f, 0.02f, 0.02f) * size;
			transform.localScale = localScale;
			
			if (size == maxSize)
			{
				Dirt dirt = (Dirt) DirtObject.GetComponent("Dirt");
				foreach (GameObject adjacentDirtObject in dirt.GetAdjacentDirtObjects())
				{
					Dirt adjacentDirt = (Dirt) adjacentDirtObject.GetComponent("Dirt");
					adjacentDirt.Plantable = true;
				}
			}
			
			string text = growthFactor.ToString();
			if (growthFactor >= 0.0f)
			{
				text = "+" + growthFactor;
			}
			else
			{
				text = "-" + growthFactor;
			}
			
			Vector3 screenPosition = Camera.current.WorldToScreenPoint(DirtObject.transform.position);
			GameObject.Find("The Game").GetComponent<FloatingText>().Display(text, new Vector2(screenPosition.x, screenPosition.y));
		}	
	}
	
	public void Init(Dictionary<Nutrient, int> minimumNutrients, Dictionary<Nutrient, int> optimumNutrients)
	{
		this.minimumNutrients = minimumNutrients;
		this.optimumNutrients = optimumNutrients;
		this.providedNutrients = new Dictionary<Nutrient, int>();
		this.providedNutrientRanges = new Dictionary<Nutrient, int>();
		this.yieldFrequency = 0.0f;
		this.yieldValue = 0.0f;
	}
	
	public void Init(Dictionary<Nutrient, int> minimumNutrients, Dictionary<Nutrient, int> optimumNutrients,
		Dictionary<Nutrient, int> providedNutrients, Dictionary<Nutrient, int> providedNutrientRanges)
	{
		this.minimumNutrients = minimumNutrients;
		this.optimumNutrients = optimumNutrients;
		this.providedNutrients = providedNutrients;
		this.providedNutrientRanges = providedNutrientRanges;
		this.yieldFrequency = 0.0f;
		this.yieldValue = 0.0f;
	}
	
	public void Init(Dictionary<Nutrient, int> minimumNutrients, Dictionary<Nutrient, int> optimumNutrients,
		float yieldValue, float yieldFrequency)
	{
		this.minimumNutrients = minimumNutrients;
		this.optimumNutrients = optimumNutrients;
		this.providedNutrients = new Dictionary<Nutrient, int>();
		this.providedNutrientRanges = new Dictionary<Nutrient, int>();
		this.yieldFrequency = yieldFrequency;
		this.yieldValue = yieldValue;
	}
	
	void Provide()
	{
		Dirt dirt = (Dirt) DirtObject.GetComponent("Dirt");
		
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

	void Start()
	{
		growthRate = 0.2f;
		lastGrowTime = Time.timeSinceLevelLoad;
		lastYieldTime = Time.timeSinceLevelLoad;
		maxSize = 5.0f;
		size = 1.0f;
		transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
	}
	
	void Update()
	{
		if (DirtObject == null)
		{
			lastGrowTime = Time.timeSinceLevelLoad;
			lastYieldTime = Time.timeSinceLevelLoad;
			return;	
		}

		if (Time.timeSinceLevelLoad - lastGrowTime > 1.0f / growthRate)
		{
			lastGrowTime = Time.timeSinceLevelLoad;
			
			float growthFactor = Consume();
			
			Provide();
			
			Grow(growthFactor);
			
			Yield();
		}
	}
	
	void Yield()
	{
		if (Time.timeSinceLevelLoad - lastYieldTime > yieldFrequency)
		{
			lastYieldTime = Time.timeSinceLevelLoad;
			
			if (size == maxSize)
			{
				GameObject theGame = GameObject.Find("The Game");
				Player player = (Player) theGame.GetComponent("Player");
				player.Earn(yieldValue);
			}
		}
	}
}
