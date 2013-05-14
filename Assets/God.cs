using UnityEngine;
using System.Collections;

public class God : MonoBehaviour
{
	public int initialWorldHeight;
	
	public int initialWorldWidth;
	
	void Start()
	{
		Application.runInBackground = true;
		GameObject[,] dirtObjects = new GameObject[initialWorldWidth, initialWorldHeight];
		
		for (int column = 0; column < initialWorldWidth; column++)
		{
			for (int row = 0; row < initialWorldHeight; row++)
			{
				GameObject dirtObject = (GameObject) Instantiate(GameObject.Find("Dirt"));
				dirtObject.transform.position =
					new Vector3(initialWorldWidth * -0.5f + column + 0.5f, initialWorldHeight * -0.5f + row + 0.5f, 0.0f);
				dirtObjects[column, row] = dirtObject;
			}
		}
		
		for (int column = 0; column < initialWorldWidth; column++)
		{
			for (int row = 0; row < initialWorldHeight; row++)
			{
				Dirt dirt = dirtObjects[column, row].GetComponent<Dirt>();
				
				if (column == initialWorldWidth / 2 &&
					row == initialWorldHeight / 2)
				{
					dirt.SetPlantable();
				}
				
				if (column != 0)
				{
					Dirt adjacentDirt = dirtObjects[column - 1, row].GetComponent<Dirt>();
					dirt.AddAdjacentDirtObject(adjacentDirt.gameObject);
					adjacentDirt.AddAdjacentDirtObject(dirt.gameObject);
				}
				
				if (row != 0)
				{
					Dirt adjacentDirt = dirtObjects[column, row - 1].GetComponent<Dirt>();
					dirt.AddAdjacentDirtObject(adjacentDirt.gameObject);
					adjacentDirt.AddAdjacentDirtObject(dirt.gameObject);
				}
				
				if (column != 0 &&
					row != 0)
				{
					Dirt adjacentDirt = dirtObjects[column - 1, row - 1].GetComponent<Dirt>();
					dirt.AddAdjacentDirtObject(adjacentDirt.gameObject);
					adjacentDirt.AddAdjacentDirtObject(dirt.gameObject);
				}
				
				if (column != 0 &&
					row != initialWorldHeight - 1)
				{
					Dirt adjacentDirt = dirtObjects[column - 1, row + 1].GetComponent<Dirt>();
					dirt.AddAdjacentDirtObject(adjacentDirt.gameObject);
					adjacentDirt.AddAdjacentDirtObject(dirt.gameObject);
				}
			}
		}
	}
	
	void Update()
	{
	}
}
