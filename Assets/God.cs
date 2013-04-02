using UnityEngine;
using System.Collections;

public class God : MonoBehaviour
{
	public int initialWorldHeight;
	
	public int initialWorldWidth;
	
	void Start()
	{
		for (int column = 0; column < initialWorldWidth; column++)
		{
			for (int row = 0; row < initialWorldHeight; row++)
			{
				GameObject dirt = (GameObject) Instantiate(GameObject.Find("Dirt"));
				dirt.transform.position = new Vector3(initialWorldWidth * -0.5f + column + 0.5f, initialWorldHeight * -0.5f + row + 0.5f, 0.0f);
			}
		}
	}
	
	void Update()
	{
	}
}
