using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other) {
		// This is really stupid and it is destroying a bunch of things it shoudn't. Take more care!
		if (other.tag == "Asteroid") {
			Destroy (other.gameObject);
		}
	}
}
