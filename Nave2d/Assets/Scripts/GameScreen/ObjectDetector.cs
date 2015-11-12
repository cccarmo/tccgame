using UnityEngine;
using System.Collections;

public class ObjectDetector : MonoBehaviour {

	public int hittingType = 0;
	public int hitting1 = 0;
	public int hitting2 = 0;
	public int hitting3 = 0;


//	void Start() {
//		Debug.Log("START");
//		hittingType = 0;
//		hitting1 = false;
//		hitting2 = false;
//		hitting3 = false;
//	}

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
		Debug.Log (other.tag);
		if (other.tag != "Boundary") {
			if (other.tag == "Asteroid") {
				hitting1++;
			} else if (other.tag == "Collectible") {
				Debug.Log("PILHA");
				hitting3++;
			} else if (other.tag == "Obstacle") {
				hitting2++;
			} 
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag != "Boundary") {
			if (other.tag == "Asteroid") {
				hitting1--;
			} else if (other.tag == "Collectible") {
				hitting3--;
			} else if (other.tag == "Obstacle") {
				hitting2--;
			} 
		}
	}

	public bool getCollisionType (int type) {

		switch (type) {
		case 1:
			if (hitting1 > 0)
				return true;
			break;
		case 2:
			if (hitting2 > 0)
				return true;
			break;
		case 3:
			if (hitting3 > 0)
				return true;
			break;
		};
		return false;
	}
}
