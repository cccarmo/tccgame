using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
	public GameObject collectionParticle;
	public GameObject shotExplosion;
	private GameObject gameScreen;
	private bool isOnDetector = false;
	private Collider2D detector;
	private bool alreadyDestroyed = false;

	void Start() {
		gameScreen = GameObject.FindWithTag("GameScreen");
		Rigidbody2D body = GetComponentInChildren<Rigidbody2D> ();
		body.angularVelocity = 50f;
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "ObjectDetector") {
			isOnDetector = true;
			detector = collider;
		}
		if (collider.tag == "Player") {
			GameObject brightParticle = GameObject.Instantiate(collectionParticle, transform.position, transform.rotation) as GameObject;
			brightParticle.transform.parent = gameScreen.transform;
			Destroy(this.gameObject, 0.1f);
			if (isOnDetector && !alreadyDestroyed) {
				alreadyDestroyed = true;
				detector.GetComponent<ObjectDetector>().hitting3--;
			}
		}
		else if (collider.tag == "Shot") {
			GameObject newExplosion = GameObject.Instantiate(shotExplosion, collider.transform.position, collider.transform.rotation) as GameObject;
			newExplosion.transform.parent = gameScreen.transform;
			collider.transform.parent = gameScreen.transform;
			Destroy(collider.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.tag == "ObjectDetector") {
			isOnDetector = false;
		}
	}

}
