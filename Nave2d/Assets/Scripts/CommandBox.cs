using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CommandBox : MonoBehaviour {
	public string label;
	public CommandCreator commandCreator;
	private Text commandText;
	private Vector3 originalPosition;
	private Vector3 mousePosition;
	public float moveSpeed = 1f;
	private bool dragging = false;

	public void setLabelBox(string commandLabel) {
		commandText = gameObject.GetComponentInChildren<Text>();
		commandText.text = commandLabel;
	}
	
	void Update() {
		if(dragging) {
			mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
			transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
		}
	}

	void OnMouseDown() {
		originalPosition = transform.position;
		dragging = true;
	}

	void OnMouseUpAsButton() {
		dragging = false;
		commandCreator.handleEvent(label);
		transform.position = originalPosition;
	}
}
