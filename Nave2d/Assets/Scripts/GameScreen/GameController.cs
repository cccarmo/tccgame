﻿using UnityEngine;
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
	private bool gameOver;
	private bool startedSimulation;
	public GameObject gameScreen;
	
	IEnumerator spawnWaves() {
		yield return new WaitForSeconds (startWait);
		while (!gameOver) {
			for (int i = 0; i < hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (spawnValuesMIN.x, spawnValuesMAX.x), spawnValuesMIN.y, spawnValuesMIN.z);
				Quaternion spawnRotation = hazard.transform.rotation;
				GameObject newAsteroid = GameObject.Instantiate (hazard, spawnPosition, spawnRotation) as GameObject;
				newAsteroid.transform.parent = gameScreen.transform;
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
	}

	void Start() {
		gameOver = startedSimulation = false;
		LevelController.setLevelReady ();
	}
	
	public void GameOver() {
		gameOver = true;
	}

	void Update() {
		if (startedSimulation && Input.GetKeyDown(KeyCode.R))
			StartCoroutine(spawnWaves());
	}
}

