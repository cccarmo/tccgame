using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CommandFactory : MonoBehaviour {
	private bool dragging;
	private bool clicked;
	private CommandCreator commandCreator;
	public PanelListener listener;
	public string eventType;
	public GameObject box;

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
			CommandBox commandBox = box.GetComponent<CommandBox>();
			commandBox.onRelease();
		}
		clicked = dragging = false;
	}

	void OnMouseExit() {
		if(clicked) {
			clicked = false;
			dragging = true;

			box = commandCreator.handleEvent(eventType);
			
			var pointer = new PointerEventData(EventSystem.current);
			CommandBox commandBox = box.GetComponent<CommandBox>();

			box.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			commandBox.onClick();
		}
	}
}
