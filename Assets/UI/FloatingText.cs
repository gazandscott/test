using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloatingText : MonoBehaviour
{	
	List<string> texts;
	List<float> textAges;
	List<Color> textColors;
	List<Vector2> textLocations;
	
	public void Display(string text, Color color, Vector2 location)
	{
		texts.Add(text);
		textAges.Add(0.0f);
		textColors.Add(color);
		textLocations.Add(location);
	}
	
	void OnGUI()
	{
		for (int index = 0; index < texts.Count; index++)
		{
			string text = texts[index];
			textAges[index] += Time.deltaTime;
			float textAge = textAges[index];
			Color textColor = textColors[index];
			Vector2 textLocation = textLocations[index];
			
			if (textAge > 5.0f)
			{
				texts.RemoveAt(index);
				textAges.RemoveAt(index);
				textColors.RemoveAt(index);
				textLocations.RemoveAt(index);
				index--;
				continue;	
			}
			
			Color origColor = GUI.skin.label.normal.textColor;
			GUI.skin.label.normal.textColor = textColor;
			GUI.Label(new Rect(textLocation.x, Screen.height - (textLocation.y + textAge * 7.5f), 200, 200), text);
			GUI.skin.label.normal.textColor = origColor;
		}
	}

	void Start()
	{
		texts = new List<string>();
		textAges = new List<float>();
		textColors = new List<Color>();
		textLocations = new List<Vector2>();
	}
	
	void Update()
	{
	}
}
