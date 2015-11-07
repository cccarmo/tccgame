using UnityEngine;
using System.Collections;

public class DestroyByContact : Scheduler {
	public GameObject playerExplosion;
	public GameObject shotExplosion;
	private GameObject gameScreen;
	private CommandInterpreter commandInterpreter;

	void Start() {
		gameScreen = GameObject.FindWithTag("GameScreen");
		GameObject panel = GameObject.FindWithTag("DropPanel");
		commandInterpreter = panel.GetComponent<CommandInterpreter>();
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
		commandInterpreter.setRestart(true);
	}
}
