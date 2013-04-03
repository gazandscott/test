using UnityEngine;
using System.Collections;

public class WateringCan : MonoBehaviour {
	
	
	public LayerMask layerMask;
	GameObject clickedGameObj;
	bool clickedObjAquired;
	Dirt dirt;
	
	void OnGUI()
	{		
		if(Event.current.type == EventType.mouseUp && Event.current.button == 1)
		{
			clickedGameObj = GetClickedGameObject();
			
			if(clickedGameObj.name.StartsWith("Dirt"))
			{
				// Particles for watering schtuff
				GameObject WaterDroplets = (GameObject) Instantiate(GameObject.Find("Water Drops"));
				WaterDroplets.transform.position = clickedGameObj.transform.position - new Vector3(0,0,1);
			}
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
