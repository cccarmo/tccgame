using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	public GameObject playerExplosion;
	public GameObject shotExplosion;
	private GameController gameController;
	private GameObject gameScreen;
	private 

	void Start() {
		gameScreen = GameObject.FindWithTag("GameScreen");
		gameController = gameScreen.GetComponent<GameController>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			GameObject newExplosion = GameObject.Instantiate(playerExplosion, collider.transform.position, collider.transform.rotation) as GameObject;
			collider.transform.parent = gameScreen.transform;
			Destroy(collider.gameObject);
			StartCoroutine(restartGameAfterSeconds());
		}
		else if (collider.tag == "Shot") {
			GameObject newExplosion = GameObject.Instantiate(shotExplosion, collider.transform.position, collider.transform.rotation) as GameObject;
			collider.transform.parent = gameScreen.transform;
			Destroy(collider.gameObject);
		}
	}

	private IEnumerator restartGameAfterSeconds () {
		yield return new WaitForSeconds (2.5f);
		gameController.restart();
	}
}
