using UnityEngine;
using System;
using System.Collections.Generic;

public class PlantFactory : MonoBehaviour
{
	public Material cloverMaterial;
	
	public Material flowerMaterial;
	
	public Material vegetableMaterial;
	
	public GameObject Create(Species species)
	{
		if (species == Species.CLOVER)
		{
			return CreateClover();
		}
		else if (species == Species.FLOWER)
		{
			return CreateFlower();
		}
		else if (species == Species.VEGETABLE)
		{
			return CreateVegetable();
		}
		
		return null;
	}
	
	public GameObject CreateClover()
	{
		GameObject plantObject = (GameObject) GameObject.Instantiate(GameObject.Find("Plant"));
		Plant plant = (Plant) plantObject.GetComponent("Plant");
		plant.Species = Species.CLOVER;
		
		plantObject.renderer.material = cloverMaterial;
		
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
	
	public GameObject CreateFlower()
	{
		GameObject plantObject = (GameObject) GameObject.Instantiate(GameObject.Find("Plant"));
		Plant plant = (Plant) plantObject.GetComponent("Plant");
		plant.Species = Species.FLOWER;
		
		plantObject.renderer.material = flowerMaterial;
		
		Dictionary<Nutrient, int> minimumNutrients = new Dictionary<Nutrient, int>();
		minimumNutrients[Nutrient.H2O] = 3;
		minimumNutrients[Nutrient.N] = 1;
		
		Dictionary<Nutrient, int> optimumNutrients = new Dictionary<Nutrient, int>();
		optimumNutrients[Nutrient.H2O] = 6;
		optimumNutrients[Nutrient.N] = 2;
		
		plant.Init(minimumNutrients, optimumNutrients);
		
		return plantObject;
	}
	
	public GameObject CreateVegetable()
	{
		GameObject plantObject = (GameObject) GameObject.Instantiate(GameObject.Find("Plant"));
		Plant plant = (Plant) plantObject.GetComponent("Plant");
		plant.Species = Species.VEGETABLE;
		
		plantObject.renderer.material = vegetableMaterial;
		
		Dictionary<Nutrient, int> minimumNutrients = new Dictionary<Nutrient, int>();
		minimumNutrients[Nutrient.H2O] = 3;
		minimumNutrients[Nutrient.N] = 1;
		
		Dictionary<Nutrient, int> optimumNutrients = new Dictionary<Nutrient, int>();
		optimumNutrients[Nutrient.H2O] = 6;
		optimumNutrients[Nutrient.N] = 2;
		
		plant.Init(minimumNutrients, optimumNutrients, 2, 10);
		
		return plantObject;
	}
	
	void Start()
	{
	}
	
	void Update()
	{
	}
}
