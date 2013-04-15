using UnityEngine;
using System;
using System.Collections.Generic;

public class PlantFactory
{	
	public static GameObject createClover(Material material)
	{
		GameObject plantObject = (GameObject) GameObject.Instantiate(GameObject.Find("Plant"));
		Plant plant = (Plant) plantObject.GetComponent("Plant");
		
		plantObject.renderer.material = material;
		
		Dictionary<Nutrient, float> minimumNutrients = new Dictionary<Nutrient, float>();
		minimumNutrients[Nutrient.H2O] = 0.03f;
		minimumNutrients[Nutrient.N] = 0.01f;
		
		Dictionary<Nutrient, float> optimumNutrients = new Dictionary<Nutrient, float>();
		optimumNutrients[Nutrient.H2O] = 0.06f;
		optimumNutrients[Nutrient.N] = 0.02f;
		
		plant.Init(minimumNutrients, optimumNutrients);
		
		return plantObject;
	}
	
	public static GameObject createFlower(Material material)
	{
		GameObject plantObject = (GameObject) GameObject.Instantiate(GameObject.Find("Plant"));
		Plant plant = (Plant) plantObject.GetComponent("Plant");
		
		plantObject.renderer.material = material;
		
		Dictionary<Nutrient, float> minimumNutrients = new Dictionary<Nutrient, float>();
		minimumNutrients[Nutrient.H2O] = 0.03f;
		minimumNutrients[Nutrient.N] = 0.01f;
		
		Dictionary<Nutrient, float> optimumNutrients = new Dictionary<Nutrient, float>();
		optimumNutrients[Nutrient.H2O] = 0.06f;
		optimumNutrients[Nutrient.N] = 0.02f;
		
		plant.Init(minimumNutrients, optimumNutrients);
		
		return plantObject;
	}
	
	public static GameObject createVegetable(Material material)
	{
		GameObject plantObject = (GameObject) GameObject.Instantiate(GameObject.Find("Plant"));
		Plant plant = (Plant) plantObject.GetComponent("Plant");
		
		plantObject.renderer.material = material;
		
		Dictionary<Nutrient, float> minimumNutrients = new Dictionary<Nutrient, float>();
		minimumNutrients[Nutrient.H2O] = 0.03f;
		minimumNutrients[Nutrient.N] = 0.01f;
		
		Dictionary<Nutrient, float> optimumNutrients = new Dictionary<Nutrient, float>();
		optimumNutrients[Nutrient.H2O] = 0.06f;
		optimumNutrients[Nutrient.N] = 0.02f;
		
		plant.Init(minimumNutrients, optimumNutrients);
		
		return plantObject;
	}
}
