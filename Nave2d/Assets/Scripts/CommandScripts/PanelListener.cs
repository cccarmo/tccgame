using UnityEngine;
using System.Collections;

public class PanelListener : MonoBehaviour {
	public CommandCreator commandCreator;
	private bool listening = false;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Command")
			listening = true;
	}

	void OnTriggerExit2D(Collider2D other) {
		listening = false;
	}

	public bool isOverPanel() {
		return listening;
	}
}
