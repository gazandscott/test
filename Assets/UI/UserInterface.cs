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
	
	public GameObject SelectedDirtObject
	{
		get
		{
			return selectedDirtObject;
		}
		
		private set
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
			if (GetComponent<Repotter>().IsRepotting())
			{
				GetComponent<Repotter>().Repot();
			}
			else
			{
				GetComponent<Repotter>().Unpot();
			}
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
