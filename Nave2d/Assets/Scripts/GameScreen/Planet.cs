using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {
	public GameObject shotExplosion;
	private GameController gameController;
	private GameObject gameScreen;
		
	void Start() {
		gameScreen = GameObject.FindWithTag("GameScreen");
		gameController = gameScreen.GetComponent<GameController>();
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			GameObject[] collectibles;
			collectibles = GameObject.FindGameObjectsWithTag("Collectible");
			if (collectibles.Length == 0)
				StartCoroutine(ArriveShip(collider));

		}
		else if (collider.tag == "Shot") {
			GameObject newExplosion = GameObject.Instantiate(shotExplosion, collider.transform.position, collider.transform.rotation) as GameObject;
			newExplosion.transform.parent = gameScreen.transform;
			collider.transform.parent = gameScreen.transform;
			Destroy(collider.gameObject);
		}
	}

	private IEnumerator ArriveShip (Collider2D ship) {
		PlayerController shipController = ship.GetComponent<PlayerController>();
		shipController.ArriveAtPlanet(GetComponent<Rigidbody2D>().transform.position);
		yield return new WaitForSeconds (2.5f);
		gameController.GameOver ();
	}

}
