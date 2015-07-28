using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CommandBox : MonoBehaviour {
	private Text commandText;
	private Vector3 mousePosition;
	public float moveSpeed = 1f;
	private bool pressed = false;

	public void Init (string commandLabel) {
		commandText = gameObject.GetComponentInChildren<Text>();
		commandText.text = commandLabel;
	}
	
	void Update () {
		if (Input.GetMouseButton (0) && pressed) {
			mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
			transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
		}
	}

	void OnMouseDown () {
		pressed = true;
	}

	void OnMouseUp () {
		pressed = false;
	}


	
}
