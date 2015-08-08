using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	public GameObject playerExplosion;
	public GameObject shotExplosion;
	private GameController gameController;
	private SimulationManager simulationManager;
	private GameObject gameScreen;

	void Start() {
		gameScreen = GameObject.FindWithTag("GameScreen");
		gameController = gameScreen.GetComponent<GameController>();
		simulationManager = gameScreen.GetComponent<SimulationManager>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			GameObject newExplosion = GameObject.Instantiate(playerExplosion, collider.transform.position, collider.transform.rotation) as GameObject;
			newExplosion.transform.parent = gameScreen.transform;
			collider.transform.parent = gameScreen.transform;
			Destroy(collider.gameObject);
			restartGameAfterSeconds();
		}
		else if (collider.tag == "Shot") {
			GameObject newExplosion = GameObject.Instantiate(shotExplosion, collider.transform.position, collider.transform.rotation) as GameObject;
			newExplosion.transform.parent = gameScreen.transform;
			collider.transform.parent = gameScreen.transform;
			Destroy(collider.gameObject);
		}
	}

	public void restartGameAfterSeconds() {
		StartCoroutine(gameController.sleepFor(2.5f));
		simulationManager.restart();
	}
}
