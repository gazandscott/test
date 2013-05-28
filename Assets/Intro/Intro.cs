using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour
{
	public Material introMaterial1;
	public Material introMaterial2;
	public Material introMaterial3;
	
	int introSlide;
	
	void OnGUI()
	{
		if (Event.current.type == EventType.KeyUp ||
			Event.current.type == EventType.MouseUp)
		{
			introSlide++;
			GameObject plane = GameObject.Find("Plane");
			
			if (introSlide == 2)
			{
				plane.renderer.material = introMaterial2;
			}
			else if (introSlide == 3)
			{
				plane.renderer.material = introMaterial3;
			}
			else if (introSlide == 4)
			{
				Application.LoadLevel("Main Scene");
			}
		}
	}
	
	void Start()
	{
		introSlide = 1;
		
		GameObject plane = GameObject.Find("Plane");
		plane.renderer.material = introMaterial1;
	}
	
	void Update()
	{
	}
}
