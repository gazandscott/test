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
	
	public List<GameObject> PlantsReceived
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
		PlantsReceived = new List<GameObject>();
	}
	
	void Update()
	{
	}
}
