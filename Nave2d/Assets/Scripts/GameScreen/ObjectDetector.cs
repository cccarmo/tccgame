using UnityEngine;
using System.Collections;

public class ObjectDetector : MonoBehaviour {

	public int hittingType = 0;
	public bool hitting1 = false;
	public bool hitting2 = false;
	public bool hitting3 = false;


	void Start() {
		hittingType = 0;
		hitting1 = false;
		hitting2 = false;
		hitting3 = false;
	}
//
//	void OnTriggerStay2D(Collider2D other) {
//		if (other.tag != "Boundary") {
//			if (other.tag == "Asteroid") {
//				hittingType = 1;
//			} else if (other.tag == "Collectible") {
//				hittingType = 3;
//			} else if (other.tag == "Obstacle") {
//				hittingType = 2;
//			} 
//		}
//	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag != "Boundary") {
			if (other.tag == "Asteroid") {
				hitting1 = true;
			} else if (other.tag == "Collectible") {
				hitting3 = true;
			} else if (other.tag == "Obstacle") {
				hitting2 = true;
			} 
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag != "Boundary") {
			if (other.tag == "Asteroid") {
				hitting1 = false;
			} else if (other.tag == "Collectible") {
				hitting3 = false;
			} else if (other.tag == "Obstacle") {
				hitting2 = false;
			} 
		}
	}

	public int getCollisionType () {
		if (hitting1)
			return 1;
		else if (hitting2)
			return 2;
		else if (hitting3)
			return 3;
		else
			return 0;
	}
}
