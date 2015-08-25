using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ComparisonFactory : MonoBehaviour {
	private bool dragging;
	private bool clicked;
	private CommandCreator commandCreator;
	public string eventType;
	private GameObject box;
	public GameObject ParentPanelForBoxes;

	void Start() {
		dragging = clicked = false;
		commandCreator = GetComponentInParent<CommandCreator>();
	}

	void OnMouseDown() {
		clicked = true;
	}

	void OnMouseUp() {
		if (dragging) {
			ComparisonBox comparisonBox = box.GetComponent<ComparisonBox>();
			comparisonBox.OnMouseUp();
		}
		clicked = dragging = false;
	}

	void OnMouseExit() {
		if(clicked) {
			clicked = false;
			dragging = true;

			box = commandCreator.handleEvent(eventType);

			ComparisonBox comparisonBox = box.GetComponent<ComparisonBox>();

			box.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			comparisonBox.OnMouseDown();
		}
	}
}
