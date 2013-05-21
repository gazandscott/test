using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloatingText : MonoBehaviour
{	
	List<string> texts;
	List<float> textAges;
	List<Vector2> textLocations;
	
	public void Display(string text, Vector2 location)
	{
		texts.Add(text);
		textAges.Add(0.0f);
		textLocations.Add(location);
	}
	
	void OnGUI()
	{
		for (int index = 0; index < texts.Count; index++)
		{
			string text = texts[index];
			textAges[index] += Time.deltaTime;
			float textAge = textAges[index];
			Vector2 textLocation = textLocations[index];
			
			if (textAge > 5.0f)
			{
				texts.RemoveAt(0);
				textAges.RemoveAt(0);
				textLocations.RemoveAt(0);
				index--;
				continue;	
			}
			
			GUI.Label(new Rect(textLocation.x, Screen.height - (textLocation.y + textAge * 7.5f), 200, 200), text);
		}
	}

	void Start()
	{
		texts = new List<string>();
		textAges = new List<float>();
		textLocations = new List<Vector2>();
	}
	
	void Update()
	{
	}
}
