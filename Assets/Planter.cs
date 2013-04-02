using UnityEngine;
using System.Collections;

public class Planter : MonoBehaviour
{
    void OnGUI()
	{
        if (Event.current.type == EventType.mouseUp && Event.current.button == 0)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			LayerMask layerMask = new LayerMask();
			layerMask.value = 1;
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
			{
				if (hit.collider.name.StartsWith("Dirt"))
				{
					Dirt dirt = (Dirt) hit.collider.gameObject.GetComponent("Dirt");
					if (dirt.GetPlant() == null)
					{
						dirt.SetPlant((GameObject) Instantiate(GameObject.Find("Plant")));
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
				