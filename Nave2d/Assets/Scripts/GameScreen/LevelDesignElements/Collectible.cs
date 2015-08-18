using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {
	public GameObject collectionParticle;
	private GameObject gameScreen;
	
	void Start() {
		gameScreen = GameObject.FindWithTag("GameScreen");
		float tumble = 30f;
		Rigidbody2D body = GetComponentInChildren<Rigidbody2D> ();
		body.angularVelocity = Random.value * tumble;
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			GameObject brightParticle = GameObject.Instantiate(collectionParticle, collider.transform.position, collider.transform.rotation) as GameObject;
			brightParticle.transform.parent = gameScreen.transform;
			//collider.transform.parent = gameScreen.transform;
			Destroy(this.gameObject);
		}
	}
}
