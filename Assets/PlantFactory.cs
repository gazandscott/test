using UnityEngine;
using System;
using System.Collections.Generic;

public class PlantFactory
{	
	public static GameObject createClover(Material material)
	{
		GameObject plantObject = (GameObject) GameObject.Instantiate(GameObject.Find("Plant"));
		Plant plant = (Plant) plantObject.GetComponent("Plant");
		plant.PlantType = PlantType.CLOVER;
		
		plantObject.renderer.material = material;
		
		Dictionary<Nutrient, int> minimumNutrients = new Dictionary<Nutrient, int>();
		minimumNutrients[Nutrient.H2O] = 3;
		minimumNutrients[Nutrient.N] = 0;
		
		Dictionary<Nutrient, int> optimumNutrients = new Dictionary<Nutrient, int>();
		optimumNutrients[Nutrient.H2O] = 6;
		optimumNutrients[Nutrient.N] = 1;
		
		Dictionary<Nutrient, int> providedNutrients = new Dictionary<Nutrient, int>();
		providedNutrients[Nutrient.N] = 6;
		
		Dictionary<Nutrient, int> providedNutrientRanges = new Dictionary<Nutrient, int>();
		providedNutrientRanges[Nutrient.N] = 2;
		
		plant.Init(minimumNutrients, optimumNutrients, providedNutrients, providedNutrientRanges);
		
		return plantObject;
	}
	
	public static GameObject createFlower(Material material)
	{
		GameObject plantObject = (GameObject) GameObject.Instantiate(GameObject.Find("Plant"));
		Plant plant = (Plant) plantObject.GetComponent("Plant");
		plant.PlantType = PlantType.FLOWER;
		
		plantObject.renderer.material = material;
		
		Dictionary<Nutrient, int> minimumNutrients = new Dictionary<Nutrient, int>();
		minimumNutrients[Nutrient.H2O] = 3;
		minimumNutrients[Nutrient.N] = 1;
		
		Dictionary<Nutrient, int> optimumNutrients = new Dictionary<Nutrient, int>();
		optimumNutrients[Nutrient.H2O] = 6;
		optimumNutrients[Nutrient.N] = 2;
		
		plant.Init(minimumNutrients, optimumNutrients);
		
		return plantObject;
	}
	
	public static GameObject createVegetable(Material material)
	{
		GameObject plantObject = (GameObject) GameObject.Instantiate(GameObject.Find("Plant"));
		Plant plant = (Plant) plantObject.GetComponent("Plant");
		plant.PlantType = PlantType.VEGETABLE;
		
		plantObject.renderer.material = material;
		
		Dictionary<Nutrient, int> minimumNutrients = new Dictionary<Nutrient, int>();
		minimumNutrients[Nutrient.H2O] = 3;
		minimumNutrients[Nutrient.N] = 1;
		
		Dictionary<Nutrient, int> optimumNutrients = new Dictionary<Nutrient, int>();
		optimumNutrients[Nutrient.H2O] = 6;
		optimumNutrients[Nutrient.N] = 2;
		
		plant.Init(minimumNutrients, optimumNutrients, 2, 10);
		
		return plantObject;
	}
}
