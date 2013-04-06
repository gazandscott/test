using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plant : MonoBehaviour
{
	float age;
	
	GameObject dirtObject;
	
	float growthRate;
	
	float lastGrowTime;
	
	Dictionary<Nutrient, float> nutrientRequirements;
	
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
		age = 0.0f;
		growthRate = 0.2f;
		lastGrowTime = 0.0f;
		nutrientRequirements = new Dictionary<Nutrient, float>();
		transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
		
		nutrientRequirements[Nutrient.H2O] = 0.03f;
		nutrientRequirements[Nutrient.N] = 0.01f;
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
			Dirt dirt = (Dirt) dirtObject.GetComponent("Dirt");
			foreach (Nutrient nutrient in nutrientRequirements.Keys)
			{
				dirt.Consume(nutrient, nutrientRequirements[nutrient]);
			}
			
			if (age < 5.0f)
			{
				age++;
				lastGrowTime = Time.timeSinceLevelLoad;
				
				Vector3 localScale = new Vector3(0.02f, 0.02f, 0.02f) * age;
				transform.localScale = localScale;
			}
		}
	}
}
