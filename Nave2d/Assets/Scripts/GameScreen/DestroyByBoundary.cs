using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Asteroid") {
			Destroy (other.gameObject);
		}
		else if (other.tag == "Shot") {
			Destroy (other.gameObject);
		}
	}
}
