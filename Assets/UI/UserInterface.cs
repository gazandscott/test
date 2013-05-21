using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserInterface : MonoBehaviour
{
	public Texture cloverTexture;
	public Texture fertilizeTexture;
	public Texture marigoldTexture;
	public Texture repotTexture;
	public Texture tomatoTexture;
	public Texture tradeTexture;
	public Texture waterTexture;
	
	GameObject selectedDirtObject;
	
	GUIStyle style;
	
	Species toBePlanted;
	
	public GameObject SelectedDirtObject
	{
		get
		{
			return selectedDirtObject;
		}
		
		set
		{
			if (selectedDirtObject != null)
			{
				selectedDirtObject.renderer.material.color = new Color(200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, 1.0f);
			}
			
			selectedDirtObject = value;
			selectedDirtObject.renderer.material.color = Color.white;
		}
	}
	
	void Awake()
	{
		selectedDirtObject = null;
		style = new GUIStyle();
		style.padding = new RectOffset(0, 0, 0, 0);
	}
	
	public int DrawActions()
	{
		int x = 0;
		
		if (GUI.Button(new Rect(x, 0, fertilizeTexture.width, fertilizeTexture.height), fertilizeTexture, style))
		{
			GetComponent<Fertilizer>().Fertilize();
		}
		
		x += fertilizeTexture.width;
		if (GUI.Button(new Rect(x, 0, repotTexture.width, repotTexture.height), repotTexture, style))
		{
			GetComponent<Potter>().Pot();
		}
		
		x += repotTexture.width;
		if (GUI.Button(new Rect(x, 0, tradeTexture.width, tradeTexture.height), tradeTexture, style))
		{
		}
		
		x += tradeTexture.width;
		if (GUI.Button(new Rect(x, 0, waterTexture.width, waterTexture.height), waterTexture, style))
		{
			GetComponent<WateringCan>().Water();
		}
		x += waterTexture.width;
		
		return fertilizeTexture.height;
	}
	
	public void DrawAvailablePlants(int y)
	{
		if (GUI.Button(new Rect(0, y, cloverTexture.width, cloverTexture.height), cloverTexture, style))
		{
			GetComponent<Planter>().Plant(Species.CLOVER);
		}
		y += cloverTexture.height;
		
		if (GUI.Button(new Rect(0, y, marigoldTexture.width, marigoldTexture.height), marigoldTexture, style))
		{
			GetComponent<Planter>().Plant(Species.MARIGOLD);
		}
		y += marigoldTexture.height;
		
		if (GUI.Button(new Rect(0, y, tomatoTexture.width, tomatoTexture.height), tomatoTexture, style))
		{
			GetComponent<Planter>().Plant(Species.TOMATO);
		}
		y += tomatoTexture.height;
		
		y += 50;
		
		List<GameObject> receivedAndPlanted = new List<GameObject>();
		
		foreach (GameObject receivedPlantObject in GetComponent<Player>().UnplantedPlants)
		{
			if (receivedPlantObject.GetComponent<Plant>().Species == Species.CLOVER)
			{
				if (GUI.Button(new Rect(0, y, cloverTexture.width, cloverTexture.height), cloverTexture, style))
				{
					GetComponent<Planter>().Plant(receivedPlantObject);
					receivedAndPlanted.Add(receivedPlantObject);
				}
				y += cloverTexture.height;
			}
			
			if (receivedPlantObject.GetComponent<Plant>().Species == Species.MARIGOLD)
			{
				if (GUI.Button(new Rect(0, y, marigoldTexture.width, marigoldTexture.height), marigoldTexture, style))
				{
					GetComponent<Planter>().Plant(receivedPlantObject);
					receivedAndPlanted.Add(receivedPlantObject);
				}
				y += marigoldTexture.height;
			}
			
			if (receivedPlantObject.GetComponent<Plant>().Species == Species.TOMATO)
			{
				if (GUI.Button(new Rect(0, y, tomatoTexture.width, tomatoTexture.height), tomatoTexture, style))
				{
					GetComponent<Planter>().Plant(receivedPlantObject);
					receivedAndPlanted.Add(receivedPlantObject);
				}
				y += tomatoTexture.height;
			}
		}
		
		foreach (GameObject receivedPlantObject in receivedAndPlanted)
		{
			GetComponent<Player>().UnplantedPlants.Remove(receivedPlantObject);
		}
	}
	
	void OnGUI()
	{
		int actionsHeight = DrawActions();
		DrawAvailablePlants(actionsHeight);
		
		if(Event.current.type == EventType.mouseUp && Event.current.button == 0)
		{
			GameObject clickedObject = Utils.GetGameObjectAtMousePosition();
			if (clickedObject != null)
			{
				if (clickedObject.name.StartsWith("Dirt"))
				{
					Dirt dirt = clickedObject.GetComponent<Dirt>();
					if (dirt.Plantable)
					{
						SelectedDirtObject = clickedObject;
					}
				}
				else if (clickedObject.name.StartsWith("Plant"))
				{
					GameObject dirtObject = clickedObject.GetComponent<Plant>().DirtObject;
					Dirt dirt = dirtObject.GetComponent<Dirt>();
					if (dirt.Plantable)
					{
						SelectedDirtObject = dirtObject;
					}
				}
			}
		}
	}
	
	void Start()
	{
	}
	
	void Update()
	{
	}
}
