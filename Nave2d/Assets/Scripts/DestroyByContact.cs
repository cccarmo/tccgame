using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;
	private GameController gameController;
	private GameObject GameScreen;


	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		GameScreen = GameObject.FindWithTag ("GameScreen");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Boundary") {
			return;
		}
		GameObject newExplosion1 = GameObject.Instantiate (explosion, transform.position, transform.rotation) as GameObject;
		newExplosion1.transform.parent = GameScreen.transform;
		if (other.tag == "Player") {
			GameObject newExplosion2 = GameObject.Instantiate (playerExplosion, other.transform.position, other.transform.rotation) as GameObject;
			newExplosion2.transform.parent = GameScreen.transform;
			gameController.GameOver();
		} else {
			gameController.AddScore (10);
		}
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
