using UnityEngine;
using System.Collections;

public class God : MonoBehaviour
{
	const float RAIN_FREQUENCY = 10.0f;
	
	public int initialWorldHeight;
	
	public int initialWorldWidth;
	
	float lastRainTime;
	
	void Awake()
	{
		Application.runInBackground = true;
		lastRainTime = 0.0f;
	}
	
	void Start()
	{
		GameObject[,] dirtObjects = new GameObject[initialWorldWidth, initialWorldHeight];
		
		for (int column = 0; column < initialWorldWidth; column++)
		{
			for (int row = 0; row < initialWorldHeight; row++)
			{
				GameObject dirtObject = (GameObject) Instantiate(GameObject.Find("Dirt"));
				dirtObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
				dirtObject.transform.Translate(new Vector3(initialWorldWidth * -0.5f + column + 0.5f, initialWorldHeight * -0.5f + row + 0.5f, 0.0f));
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
					dirt.Plantable = true;
					GetComponent<UserInterface>().SelectedDirtObject = dirtObjects[column, row];
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
		if (Time.timeSinceLevelLoad - lastRainTime > RAIN_FREQUENCY)
		{
			lastRainTime = Time.timeSinceLevelLoad;
			
			if (Random.Range(0.0f, 1.0f) > 0.5f)
			{
				foreach (GameObject dirtObject in GameObject.FindGameObjectsWithTag("Dirt"))
				{
					Dirt dirt = dirtObject.GetComponent<Dirt>();
					dirt.Provide(Nutrient.H2O, 100);
					
					// Particles for watering schtuff
					GameObject WaterDroplets = (GameObject) Instantiate(GameObject.Find("Water Drops"));
					Vector3 waterPosition = dirtObject.transform.position;
					waterPosition.z = -4.0f;
					waterPosition.y += Dirt.EXTENT * 0.5f;
					WaterDroplets.transform.position = waterPosition;
				}
			}
		}
	}
}
