using UnityEngine;
using System.Collections;

public class ObjectDetector : MonoBehaviour {

	private int hittingType = 0;

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag != "Boundary") {
			if (other.tag == "Asteroid") {
				hittingType = 1;
			} else if (other.tag == "Collectible") {
				hittingType = 3;
			} else if (other.tag == "Obstacle") {
				hittingType = 2;
			} 
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag != "Boundary") {
			if (other.tag == "Asteroid") {
				hittingType = 1;
			} else if (other.tag == "Collectible") {
				hittingType = 3;
			} else if (other.tag == "Obstacle") {
				hittingType = 2;
			} 
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag != "Boundary") {
			hittingType = 0;
		}
		Debug.Log (hittingType);
	}

	public int getCollisionType () {
		return hittingType;
	}
}
