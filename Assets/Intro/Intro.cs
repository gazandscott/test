using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour
{
	public Material introMaterial1;
	public Material introMaterial2;
	public Material introMaterial3;
	public Material introMaterial4;
	
	int introSlide;
	
	void OnGUI()
	{
		if (Event.current.type == EventType.KeyUp ||
			Event.current.type == EventType.MouseUp)
		{
			introSlide++;
			GameObject plane = GameObject.Find("Plane");
			Material[] materials = plane.renderer.materials;
			
			if (introSlide == 2)
			{
				materials[1] = introMaterial2;
			}
			else if (introSlide == 3)
			{
				materials[1] = introMaterial3;
			}
			else if (introSlide == 4)
			{
				materials[1] = introMaterial4;
			}
			else if (introSlide == 5)
			{
				Application.LoadLevel("Main Scene");
			}
			
			plane.renderer.materials = materials;
		}
	}
	
	void Start()
	{
		introSlide = 1;
		
		GameObject plane = GameObject.Find("Plane");
		Material[] materials = plane.renderer.materials;
		materials[1] = introMaterial1;
		plane.renderer.materials = materials;
	}
	
	void Update()
	{
	}
}
