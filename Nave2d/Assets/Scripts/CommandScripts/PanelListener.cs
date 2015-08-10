using UnityEngine;
using System.Collections;

public class PanelListener : MonoBehaviour {
	public CommandCreator commandCreator;
	private bool listening = false;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "DropCommand")
			listening = true;
	}

	void OnTriggerExit2D(Collider2D other) {
		listening = false;
	}

	public void handleEvent(string eventType) {
		if(listening)
			commandCreator.handleEvent(eventType);
	}
}
