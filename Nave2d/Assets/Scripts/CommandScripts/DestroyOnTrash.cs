using UnityEngine;
using System.Collections;

public class DestroyOnTrash : MonoBehaviour {
	private CommandInterpreter commandInterpreter;
	public CommandBox box;
	private bool delete = false;

	void Start() {
		commandInterpreter = this.GetComponentInParent<CommandInterpreter>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "TrashCan" && box.isActive)
			delete = true;
		else if(collider.tag == "DropPanel" && box.isActive)
			delete = false;
	}

	void OnTriggerExit2D(Collider2D collider) {
		if(collider.tag == "TrashCan")
			delete = false;
		else if(collider.tag == "DropPanel")
			delete = true;
	}

	void OnMouseUp(){
		if(delete) {
			CommandBox[] children = GetComponentsInChildren<CommandBox>();
			foreach(CommandBox child in children) {
				commandInterpreter.removeFromList(child.transform.gameObject);
			}
		}
	}

	public void onRelease() {
		OnMouseUp();
	}
}
