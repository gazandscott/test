using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RootGame : MonoBehaviour
{	
	public Material nitrogenMaterial;
	
	public Transform nutrient;
	
	public Transform root;
	
	public Transform rootExtension;
	
	float rootSelectionTime;
	
	int score;
	
	GameObject selectedRoot;
	
	public float swipeTime;
	
	public Material waterMaterial;
	
	void OnGUI()
	{
		GUI.Box(new Rect(0, 0, 100, 105), "", Utils.GetGUIStyle());
		
		GUI.Label(new Rect(10, 10, 80, 20), "Score: " + score);
		
		if (Event.current.type == EventType.mouseDown && Event.current.button == 0)
		{
			GameObject clickedObject = Utils.GetGameObjectAtMousePosition();
			if (clickedObject != null && clickedObject.name.StartsWith("Root"))
			{
				rootSelectionTime = Time.timeSinceLevelLoad;
				selectedRoot = clickedObject;
			}
		}
	}
	
	void Start()
	{
		rootSelectionTime = 0.0f;
		score = 0;
		selectedRoot = null;
		
		// Roots.
		GameObject root0 = ((Transform) Instantiate(root, new Vector3(-3.0f, 3.0f, 0.0f), Quaternion.identity)).gameObject;
		root0.GetComponent<RootGameRoot>().SetNutrient(Nutrient.N);
		root0.renderer.material = nitrogenMaterial;
		GameObject root1 = ((Transform) Instantiate(root, new Vector3(-1.0f, 3.0f, 0.0f), Quaternion.identity)).gameObject;
		root1.GetComponent<RootGameRoot>().SetNutrient(Nutrient.H2O);
		root1.renderer.material = waterMaterial;
		GameObject root2 = ((Transform) Instantiate(root, new Vector3(1.0f, 3.0f, 0.0f), Quaternion.identity)).gameObject;
		root2.GetComponent<RootGameRoot>().SetNutrient(Nutrient.H2O);
		root2.renderer.material = waterMaterial;
		GameObject root3 = ((Transform) Instantiate(root, new Vector3(3.0f, 3.0f, 0.0f), Quaternion.identity)).gameObject;
		root3.GetComponent<RootGameRoot>().SetNutrient(Nutrient.N);
		root3.renderer.material = nitrogenMaterial;
		
		// Nutrients.
		GameObject nutrient0 = ((Transform) Instantiate(nutrient, new Vector3(-3.0f, -3.0f, 0.0f), Quaternion.identity)).gameObject;
		nutrient0.GetComponent<RootGameNutrient>().SetNutrient(Nutrient.H2O);
		nutrient0.renderer.material = waterMaterial;
		GameObject nutrient1 = ((Transform) Instantiate(nutrient, new Vector3(-1.0f, -3.0f, 0.0f), Quaternion.identity)).gameObject;
		nutrient1.GetComponent<RootGameNutrient>().SetNutrient(Nutrient.N);
		nutrient1.renderer.material = nitrogenMaterial;
		GameObject nutrient2 = ((Transform) Instantiate(nutrient, new Vector3(1.0f, -3.0f, 0.0f), Quaternion.identity)).gameObject;
		nutrient2.GetComponent<RootGameNutrient>().SetNutrient(Nutrient.H2O);
		nutrient2.renderer.material = waterMaterial;
		GameObject nutrient3 = ((Transform) Instantiate(nutrient, new Vector3(3.0f, -3.0f, 0.0f), Quaternion.identity)).gameObject;
		nutrient3.GetComponent<RootGameNutrient>().SetNutrient(Nutrient.N);
		nutrient3.renderer.material = nitrogenMaterial;
	}
	
	void Update()
	{
		if (selectedRoot != null && Time.timeSinceLevelLoad - rootSelectionTime > swipeTime)
		{
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector3 toMouse = mousePosition - selectedRoot.transform.position;
			toMouse.z = 0.0f;
			
			if (toMouse.magnitude > 1.0f)
			{
				GameObject rootExtension0 = ((Transform) Instantiate(rootExtension, selectedRoot.transform.position + toMouse * 0.5f, Quaternion.identity)).gameObject;
				rootExtension0.renderer.material = selectedRoot.renderer.material;
				rootExtension0.transform.localScale = new Vector3(0.5f, toMouse.magnitude / 2.0f, 0.5f);
				
				float angleRadians = (float) Math.Acos(Vector3.Dot(new Vector3(0.0f, 1.0f, 0.0f), toMouse) / toMouse.magnitude);
				float angleDegrees = angleRadians / (float) Math.PI * 180.0f;
				if (mousePosition.x > selectedRoot.transform.position.x)
				{
					angleDegrees *= -1.0f;
				}
				rootExtension0.transform.Rotate(0.0f, 0.0f, angleDegrees);
				
				int extensionScore = 0;
				RaycastHit[] hits = Physics.CapsuleCastAll(selectedRoot.transform.position, mousePosition, 0.5f, toMouse);
				foreach (RaycastHit hit in hits)
				{
					// CapsuleCastAll moves the capsule in a direction looking for all collisions. Since we don't want
					// to include collisions past the extent of the capsule's original position, we'll exclude them.
					if (hit.distance <= toMouse.magnitude)
					{
						if (hit.transform.gameObject.name.StartsWith("Root Extension") &&
							hit.transform.gameObject != rootExtension0)
						{
							Destroy(rootExtension0);
							extensionScore = 0;
							break;
						}
						else if (hit.transform.gameObject.name.StartsWith("Nutrient") &&
							hit.transform.gameObject.GetComponent<RootGameNutrient>().GetNutrient() ==
							selectedRoot.GetComponent<RootGameRoot>().GetNutrient())
						{
							extensionScore += 10;
						}
					}
				}
				
				score += extensionScore;
			}
			
			selectedRoot = null;
		}
	}
}
