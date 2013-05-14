using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	float money;
	
	public void Earn(float income)
	{
		money += income;
	}
	
	public float GetMoney()
	{
		return money;	
	}
	
	public bool Spend(float expense)
	{
		if (expense > money)
		{
			return false;
		}
		
		money -= expense;
		
		return true;
	}
	
	void Start()
	{
		money = 100000.0f;
	}
	
	void Update()
	{
	}
}
