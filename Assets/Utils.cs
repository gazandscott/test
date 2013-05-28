using UnityEngine;
using System;
using System.Collections.Generic;

public class Utils
{
	public static GameObject GetGameObjectClosestToMouse(GameObject a, GameObject b)
	{
		if (a == null)
		{
			return b;
		}
		if (b == null)
		{
			return a;
		}
		
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = 0.0f;
		Vector3 aPosition = a.transform.position;
		aPosition.z = 0.0f;
		Vector3 bPosition = b.transform.position;
		bPosition.z = 0.0f;
		
		if ((mousePosition - aPosition).sqrMagnitude < (mousePosition - bPosition).sqrMagnitude)
		{
			return a;
		}
		
		return b;
	}
	
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
	
	public static List<GameObject> GetGameObjectsAtMousePosition()
	{
		List<GameObject> gameObjects = new List<GameObject>();
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		LayerMask layerMask = new LayerMask();
		layerMask.value = 1;
		
		RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity, layerMask);
		foreach (RaycastHit hit in hits)
		{
	        gameObjects.Add(hit.transform.gameObject);
		}
		
		return gameObjects;
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
