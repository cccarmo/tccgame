using UnityEngine;
using System.Collections;

public class DestroyAsteroidByContact : MonoBehaviour {
	public GameObject playerExplosion;
	public GameObject shotExplosion;
	private GameController gameController;
	private GameObject gameScreen;
	
	void Start() {
		gameScreen = GameObject.FindWithTag("GameScreen");
		gameController = gameScreen.GetComponent<GameController>();
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			GameObject newExplosion = GameObject.Instantiate(playerExplosion, collider.transform.position, collider.transform.rotation) as GameObject;
			newExplosion.transform.parent = gameScreen.transform;
			collider.transform.parent = gameScreen.transform;
			Destroy(collider.gameObject);
			StartCoroutine(restartGameAfterSeconds());
		}
		else if (collider.tag == "Shot") {
			GameObject newExplosion = GameObject.Instantiate(shotExplosion, collider.transform.position, collider.transform.rotation) as GameObject;
			newExplosion.transform.parent = gameScreen.transform;
			collider.transform.parent = gameScreen.transform;
			Destroy(collider.gameObject);
			Destroy(this.gameObject);
		}
	}
	
	private IEnumerator restartGameAfterSeconds () {
		yield return new WaitForSeconds (2.5f);
		gameController.restart();
	}
}
