using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public float Money
	{
		get;
		private set;
	}
	
	float money;
	
	public List<GameObject> UnplantedPlants
	{
		get;
		private set;
	}
	
	public void Earn(float income)
	{
		Money += income;
	}
	
	public bool Spend(float expense)
	{
		if (expense > Money)
		{
			return false;
		}
		
		Money -= expense;
		
		return true;
	}
	
	void Start()
	{
		Money = 1000.0f;
		UnplantedPlants = new List<GameObject>();
	}
	
	void Update()
	{
	}
}
