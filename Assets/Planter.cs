using UnityEngine;
using System.Collections;

public class Planter : MonoBehaviour
{
    void OnGUI()
	{
        if (Event.current.type == EventType.mouseUp && Event.current.button == 0)
		{
			GameObject plant = (GameObject) Instantiate(GameObject.Find("Plant"));

			plant.transform.position = new Vector3(Event.current.mousePosition.x / Screen.width + 0.5f, -Event.current.mousePosition.y / Screen.height - 0.5f, -2);
		}
    }

	void Start()
	{
	}

	void Update()
	{
	}
}
