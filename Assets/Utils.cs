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
}
