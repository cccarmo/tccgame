using UnityEngine;
using System.Collections;

public class AsteroidCloudEmitter : MonoBehaviour {
	public GameObject asteroid;

	private float maxSpawnDistance = 0.25f;
	private readonly int maxFlowCounter = 10;

	private readonly float startWait = 1.0f;
	public float maxSpawnWait = 0.75f;
	public float waveWait = 1.0f;

	public float speed = 5.0f;
	public Vector2 direction = new Vector2(0.0f, 1.0f);
	private System.Random seed;

	IEnumerator spawnWaves() {
		yield return new WaitForSeconds(startWait);
		int flowCounter;
		while(true) {
			flowCounter = 1 + seed.Next() % maxFlowCounter;
			for(int i = 0; i < flowCounter; i++) {
				float randomX = (1 + (float)seed.NextDouble() * maxSpawnDistance) * transform.position.x;
				float randomY = (1 + (float)seed.NextDouble() * maxSpawnDistance) * transform.position.y;
				Vector3 spawnPosition = new Vector3(randomX, randomY, transform.position.z);

				Quaternion spawnRotation = asteroid.transform.rotation;
				GameObject newAsteroid = GameObject.Instantiate(asteroid, spawnPosition, spawnRotation) as GameObject;
				newAsteroid.transform.parent = transform.parent;
				newAsteroid.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
				yield return new WaitForSeconds((float)seed.NextDouble() * maxSpawnWait);
			}
			yield return new WaitForSeconds(waveWait);
		}
	}
	
	void Start() {
		seed = new System.Random();
		StartCoroutine(spawnWaves());
	}
}
