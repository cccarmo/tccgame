using UnityEngine;
using System.Collections;

public class DestroyOnTrash : MonoBehaviour {
	private CommandInterpreter commandInterpreter;
	private bool delete = true;

	void Start() {
		commandInterpreter = this.GetComponentInParent<CommandInterpreter>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if(collider.tag == "TrashCan")
			delete = true;
		else if(collider.tag == "DropPanel")
			delete = false;
	}

	void OnTriggerExit2D(Collider2D collider) {
		if(collider.tag == "TrashCan")
			delete = false;
		else if(collider.tag == "DropPanel")
			delete = true;
	}

	void OnMouseUp(){
		if(delete)
			commandInterpreter.removeFromList(transform.gameObject);
	}

	public void onRelease() {
		OnMouseUp();
	}
}
