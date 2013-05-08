using UnityEngine;
using System.Collections;

public class RootGameRoot : MonoBehaviour
{
	Nutrient nutrient;
	
	public Nutrient GetNutrient()
	{
		return nutrient;
	}
	
	public void SetNutrient(Nutrient nutrient)
	{
		this.nutrient = nutrient;
	}
	
	void Start()
	{
	}
	
	void Update()
	{
	}
}
