using UnityEngine;
using System.Collections;

public class DestroyOnTrash : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "TrashCan") {
			CommandInterpreter commandInterpreter = this.GetComponentInParent<CommandInterpreter>();
			commandInterpreter.removeFromList(transform.gameObject);
		}
	}
}
