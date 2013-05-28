using UnityEngine;
using System;
using System.Collections.Generic;

public class PlantFactory : MonoBehaviour
{
	public Material cloverMaterial1;
	public Material cloverMaterial2;
	public Material cloverMaterial3;
	
	public Material marigoldMaterial1;
	public Material marigoldMaterial2;
	public Material marigoldMaterial3;
	
	public Material tomatoMaterial1;
	public Material tomatoMaterial2;
	public Material tomatoMaterial3;
	
	public GameObject Create(Species species)
	{
		if (species == Species.CLOVER)
		{
			return CreateClover();
		}
		else if (species == Species.MARIGOLD)
		{
			return CreateMarigold();
		}
		else if (species == Species.TOMATO)
		{
			return CreateTomato();
		}
		
		return null;
	}
	
	public GameObject CreateClover()
	{
		GameObject plantObject = (GameObject) GameObject.Instantiate(GameObject.Find("Plant"));
		Plant plant = (Plant) plantObject.GetComponent("Plant");
		
		plant.Material1 = cloverMaterial1;
		plant.Material2 = cloverMaterial2;
		plant.Material3 = cloverMaterial3;
		plant.Species = Species.CLOVER;
		
		plantObject.renderer.material = cloverMaterial1;
		
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
	
	public GameObject CreateMarigold()
	{
		GameObject plantObject = (GameObject) GameObject.Instantiate(GameObject.Find("Plant"));
		Plant plant = (Plant) plantObject.GetComponent("Plant");
		
		plant.Material1 = marigoldMaterial1;
		plant.Material2 = marigoldMaterial2;
		plant.Material3 = marigoldMaterial3;
		plant.Species = Species.MARIGOLD;
		
		plantObject.renderer.material = marigoldMaterial1;
		
		Dictionary<Nutrient, int> minimumNutrients = new Dictionary<Nutrient, int>();
		minimumNutrients[Nutrient.H2O] = 3;
		minimumNutrients[Nutrient.N] = 1;
		
		Dictionary<Nutrient, int> optimumNutrients = new Dictionary<Nutrient, int>();
		optimumNutrients[Nutrient.H2O] = 6;
		optimumNutrients[Nutrient.N] = 2;
		
		plant.Init(minimumNutrients, optimumNutrients);
		
		return plantObject;
	}
	
	public GameObject CreateTomato()
	{
		GameObject plantObject = (GameObject) GameObject.Instantiate(GameObject.Find("Plant"));
		Plant plant = (Plant) plantObject.GetComponent("Plant");
		
		plant.Material1 = tomatoMaterial1;
		plant.Material2 = tomatoMaterial2;
		plant.Material3 = tomatoMaterial3;
		plant.Species = Species.TOMATO;
		
		plantObject.renderer.material = tomatoMaterial1;
		
		Dictionary<Nutrient, int> minimumNutrients = new Dictionary<Nutrient, int>();
		minimumNutrients[Nutrient.H2O] = 3;
		minimumNutrients[Nutrient.N] = 1;
		
		Dictionary<Nutrient, int> optimumNutrients = new Dictionary<Nutrient, int>();
		optimumNutrients[Nutrient.H2O] = 6;
		optimumNutrients[Nutrient.N] = 2;
		
		plant.Init(minimumNutrients, optimumNutrients, 10, 10);
		
		return plantObject;
	}
	
	void Start()
	{
	}
	
	void Update()
	{
	}
}
