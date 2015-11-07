using UnityEngine;
using System.Collections;

public class AsteroidCloudEmitter : MonoBehaviour {
	public GameObject asteroid;

	private readonly float startWait = 0.0f;
	public float minSpawnWait, maxSpawnWait;

	public float speed;
	public Vector2 direction = new Vector2();
	private System.Random seed;

	IEnumerator spawnWaves() {
		yield return new WaitForSeconds(startWait);
		while(true) {
			float randomX = transform.position.x;
			float randomY = transform.position.y;
			Vector3 spawnPosition = new Vector3(randomX, randomY, transform.position.z);

			Quaternion spawnRotation = asteroid.transform.rotation;
			GameObject newAsteroid = GameObject.Instantiate(asteroid, spawnPosition, spawnRotation) as GameObject;
			newAsteroid.transform.parent = transform.parent;
			newAsteroid.GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
			yield return new WaitForSeconds(minSpawnWait + (float)seed.NextDouble() * (maxSpawnWait - minSpawnWait));
		}
	}
	
	void Start() {
		seed = new System.Random();
		StartCoroutine(spawnWaves());
	}
}
