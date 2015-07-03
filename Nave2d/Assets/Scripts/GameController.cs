using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValuesMIN;
	public Vector3 spawnValuesMAX;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public Text scoreText;
	public int score;
	public Text restartText;
	public Text gameOverText;
	private bool gameOver;
	private bool restart;
	public GameObject gameScreen;


	IEnumerator spawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (spawnValuesMIN.x, spawnValuesMAX.x), spawnValuesMIN.y, spawnValuesMIN.z);
				Quaternion spawnRotation = hazard.transform.rotation;
				GameObject newAsteroid = GameObject.Instantiate (hazard, spawnPosition, spawnRotation) as GameObject;
				newAsteroid.transform.parent = gameScreen.transform;
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds(waveWait);

			if (gameOver) {
				restartText.text = "Press R to restart";
				restartText.gameObject.SetActive(true);
				restart = true;
				break;
			}
		}
	}

	void UpdateScore () {
		scoreText.text = "Score = " + score;
	}

	public void AddScore (int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}
	
	// Use this for initialization
	void Start () {
		gameOver = restart = false;
		restartText.gameObject.SetActive (false);
		gameOverText.gameObject.SetActive (false);
		score = 0;
		UpdateScore ();
		StartCoroutine( spawnWaves ());
	}
	
	public void GameOver () {
		gameOverText.gameObject.SetActive (true);
		gameOver = true;
	}

	void Update () {
		if (restart && Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
