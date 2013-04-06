using UnityEngine;
using System.Collections;

public class Planter : MonoBehaviour
{
    void OnGUI()
	{
        if (Event.current.type == EventType.mouseUp && Event.current.button == 0)
		{
			GameObject clickedObject = Utils.GetGameObjectAtMousePosition();
			if (clickedObject != null)
			{
				if (clickedObject.name.StartsWith("Dirt"))
				{
					Dirt dirt = (Dirt) clickedObject.GetComponent("Dirt");
					if (dirt.GetPlantObject() == null)
					{
						GameObject plantObject = (GameObject) Instantiate(GameObject.Find("Plant"));
						Plant plant = (Plant) plantObject.GetComponent("Plant");
						
						dirt.SetPlantObject(plantObject);
						plant.SetDirtObject(clickedObject);
					}
				}
			}
		}
    }

	void Start()
	{
	}

	void Update()
	{
	}
}
				