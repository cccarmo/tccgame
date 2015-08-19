using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
	public GameObject collectionParticle;
	private GameObject gameScreen;
	
	void Start() {
		gameScreen = GameObject.FindWithTag("GameScreen");
		Rigidbody2D body = GetComponentInChildren<Rigidbody2D> ();
		body.angularVelocity = 50f;
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			GameObject brightParticle = GameObject.Instantiate(collectionParticle, transform.position, transform.rotation) as GameObject;
			brightParticle.transform.parent = gameScreen.transform;
			Destroy(this.gameObject, 0.1f);
		}
	}
}
