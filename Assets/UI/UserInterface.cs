using UnityEngine;
using System.Collections;

public class UserInterface : MonoBehaviour
{
	public Texture cloverTexture;
	public Texture fertilizeTexture;
	public Texture flowerTexture;
	public Texture repotTexture;
	public Texture tradeTexture;
	public Texture vegetableTexture;
	public Texture waterTexture;
	
	GameObject selectedDirtObject;
	
	GUIStyle style;
	
	PlantType toBePlanted;
	
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
		}
		
		x += fertilizeTexture.width;
		if (GUI.Button(new Rect(x, 0, repotTexture.width, repotTexture.height), repotTexture, style))
		{
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
			GetComponent<Planter>().Plant(PlantType.CLOVER);
		}
		y += cloverTexture.height;
		
		if (GUI.Button(new Rect(0, y, flowerTexture.width, flowerTexture.height), flowerTexture, style))
		{
			GetComponent<Planter>().Plant(PlantType.FLOWER);
		}
		y += flowerTexture.height;
		
		if (GUI.Button(new Rect(0, y, vegetableTexture.width, vegetableTexture.height), vegetableTexture, style))
		{
			GetComponent<Planter>().Plant(PlantType.VEGETABLE);
		}
	}
	
	public GameObject GetSelectedDirtObject()
	{
		return selectedDirtObject;	
	}
	
	void OnGUI()
	{
		int actionsHeight = DrawActions();
		DrawAvailablePlants(actionsHeight);
		
		GameObject clickedObject = Utils.GetGameObjectAtMousePosition();
		if (clickedObject != null)
		{
			if (clickedObject.name.StartsWith("Dirt"))
			{
				Dirt dirt = clickedObject.GetComponent<Dirt>();
				if (dirt.IsPlantable())
				{
					selectedDirtObject = clickedObject;
				}
			}
			else if (clickedObject.name.StartsWith("Plant"))
			{
				Dirt dirt = clickedObject.GetComponent<Plant>().GetDirtObject().GetComponent<Dirt>();
				if (dirt.IsPlantable())
				{
					selectedDirtObject = clickedObject;
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
