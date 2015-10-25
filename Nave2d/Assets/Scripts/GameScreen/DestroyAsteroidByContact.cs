using UnityEngine;
using System.Collections;

public class DestroyAsteroidByContact : Scheduler {
	public GameObject playerExplosion;
	public GameObject shotExplosion;
	private SimulationManager simulationManager;
	private GameObject gameScreen;
	
	void Start() {
		gameScreen = GameObject.FindWithTag("GameScreen");
		simulationManager = gameScreen.GetComponent<SimulationManager>();
	}

	void Explode() {
		GameObject newExplosion = GameObject.Instantiate(shotExplosion, transform.position, transform.rotation) as GameObject;
		newExplosion.transform.parent = gameScreen.transform;
		Destroy(this.gameObject);
	}

	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			GameObject player = collider.gameObject;
			PlayerController controller = player.GetComponent<PlayerController>();

			if (controller.isShield) {
				Explode();

			} else {
				GameObject newExplosion = GameObject.Instantiate(playerExplosion, collider.transform.position, collider.transform.rotation) as GameObject;
				newExplosion.transform.parent = gameScreen.transform;
				collider.transform.parent = gameScreen.transform;
				Destroy(player);
				restartGameAfterSeconds();
			}
		}
		else if (collider.tag == "Shot") {
			Explode();
			collider.transform.parent = gameScreen.transform;
			Destroy(collider.gameObject);
		}
	}
	
	private void restartGameAfterSeconds() {
		executeAfter(2.5f, new Action(simulationManager.reloadLevel));
	}
}
