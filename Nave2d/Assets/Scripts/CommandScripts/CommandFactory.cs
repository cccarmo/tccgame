using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CommandFactory : MonoBehaviour {
	private bool dragging;
	private bool clicked;
	private CommandCreator commandCreator;
	public string eventType;
	private GameObject box;

	void Start() {
		dragging = clicked = false;
		commandCreator = GetComponentInParent<CommandCreator>();
	}

	void OnMouseDown() {
		clicked = true;
	}

	void OnMouseUp() {
		if (clicked) {
			commandCreator.handleEvent(eventType);
		} else if (dragging) {
			DestroyOnTrash removeIfNotOverPanel = box.GetComponent<DestroyOnTrash>();
			removeIfNotOverPanel.onRelease();
			box.GetComponent<CommandBox>().onRelease();
		}
		clicked = dragging = false;
	}

	void OnMouseExit() {
		if(clicked) {
			clicked = false;
			dragging = true;

			box = commandCreator.handleEvent(eventType);
			box.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			box.GetComponent<CommandBox>().onClick();
		}
	}
}
