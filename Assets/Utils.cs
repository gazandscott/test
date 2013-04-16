using UnityEngine;
using System;

public class Utils
{
	public static GameObject GetGameObjectAtMousePosition()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
		LayerMask layerMask = new LayerMask();
		layerMask.value = 1;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
		{
            return hit.transform.gameObject;
		}
        else
		{
			return null;
		}
	}
	
	public static GUIStyle GetGUIStyle()
	{
		GUIStyle style = new GUIStyle();
		
		Texture2D background = new Texture2D(1, 1);
		background.SetPixel(1, 1, new Color(1.0f, 1.0f, 1.0f, 0.5f));
		style.normal.background = background;
		
		//return style;
		return new GUIStyle();
	}
}
