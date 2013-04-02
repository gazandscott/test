using UnityEngine;
using System.Collections;

public class WateringCan : MonoBehaviour {
	
	
	public LayerMask layerMask;
	GameObject clickedGameObj;
	bool clickedObjAquired;
	
	void OnGUI()
	{		
		if(Event.current.type == EventType.mouseUp && Event.current.button == 1)
		{
			clickedGameObj = GetClickedGameObject();
			
			//clickedGameObj.GetComponent("Dirt");
			//if(clickedGameObj != null)
			//{
			//	clickedObjAquired = true;
			//}
			
			//if(clickedObjAquired == true)
			//{
				
			//}
		}
	}
	
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{	
	}
	
	GameObject GetClickedGameObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Casts the ray and get the first game object hit<br />
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
		{
			Debug.Log("Ray casting succeeded");
            return hit.transform.gameObject;
		}
        else
		{
			Debug.Log("Ray casting failed");
			return null;
		}
	}
}
