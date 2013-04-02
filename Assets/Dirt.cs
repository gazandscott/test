using UnityEngine;
using System.Collections;

public class Dirt : MonoBehaviour
{
	float nitrogen;
	
	float water;
	
	void Start()
	{
		nitrogen = 10.0f;
		water = 10.0f;
	}
	
	void Update()
	{
		nitrogen -= 0.1f * Time.deltaTime;
		water -= 0.1f * Time.deltaTime;
	}
}
