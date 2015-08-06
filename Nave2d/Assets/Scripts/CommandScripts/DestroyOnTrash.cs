using UnityEngine;
using System.Collections;

public class DestroyOnTrash : MonoBehaviour {
	bool delete = false;
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "TrashCan") {
			Debug.Log("ENTROU");
			delete = true;
		}
	}


	void OnTriggerExit2D(Collider2D collider) {
		if (collider.tag == "TrashCan") {
			Debug.Log("SAIU");
			delete = false;
		}
	}


	void OnMouseUpAsButton(){
		if (delete) {
			CommandInterpreter commandInterpreter = this.GetComponentInParent<CommandInterpreter>();
			commandInterpreter.removeFromList(transform.gameObject);
		}
	}
}
