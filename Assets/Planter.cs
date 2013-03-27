using UnityEngine;
using System.Collections;

public class Planter : MonoBehaviour
{
    void OnGUI()
	{
        if (Event.current.type == EventType.mouseUp && Event.current.button == 0)
		{
			GameObject plant = (GameObject) Instantiate(GameObject.Find("Plant"));

			Vector3 screenPosition = new Vector3(Event.current.mousePosition.x, Screen.height - Event.current.mousePosition.y, 0.0f);
			Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
			worldPosition.z = -2.0f;
			plant.transform.position = worldPosition;
		}
    }

	void Start()
	{
	}

	void Update()
	{
	}
}
