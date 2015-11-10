using UnityEngine;
using System.Collections;

public class DestroyAsteroidByContact : Scheduler {
	public GameObject playerExplosion;
	public GameObject shotExplosion;
	private GameObject gameScreen;
	private CommandInterpreter commandInterpreter;
	private bool isOnDetector = false;
	private Collider2D detector;
	
	void Start() {
		gameScreen = GameObject.FindWithTag("GameScreen");
		GameObject panel = GameObject.FindWithTag("DropPanel");
		commandInterpreter = panel.GetComponent<CommandInterpreter>();
	}

	void Explode() {
		GameObject newExplosion = GameObject.Instantiate(shotExplosion, transform.position, transform.rotation) as GameObject;
		newExplosion.transform.parent = gameScreen.transform;
		Destroy(this.gameObject);
		if (isOnDetector) {
			detector.GetComponent<ObjectDetector>().hitting1 = false;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "ObjectDetector") {
			isOnDetector = true;
			detector = collider;
		}
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

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.tag == "ObjectDetector") {
			isOnDetector = false;
		}
	}
	
	private void restartGameAfterSeconds() {
		commandInterpreter.setRestart(true);
	}
}
