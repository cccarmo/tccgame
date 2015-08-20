using UnityEngine;
using System.Collections;

public class CommandFactory : MonoBehaviour {
	private bool dragging;
	private bool clicked;
	private CommandCreator commandCreator;
	public PanelListener listener;
	public string eventType;
	
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
			//if(listener.isOverPanel())
		}
		clicked = dragging = false;
	}

	void OnMouseExit() {
		if(clicked) {
			clicked = false;
			dragging = true;
		}
	}
}
