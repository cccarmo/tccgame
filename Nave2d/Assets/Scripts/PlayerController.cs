using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Boundary {
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
}

public class PlayerController : MonoBehaviour {
	private Rigidbody2D body;
	public Boundary boundary;

	public readonly float fixedSpeed = 5.0f;
	public readonly float tilt = 3.0f;
	public readonly float fireRate = 0.25f;
	private float nextFire;
	public GameObject Shot;
	public Transform ShotSpawn;

	public GameObject GameScreen;
	public CommandInterpreter interpreter;
	private AudioSource shootSound;
	private bool startedSimulation;

	void Start() {
		body = GetComponent<Rigidbody2D>();
		shootSound = GetComponent<AudioSource>();
		nextFire = 0.0f;
		startedSimulation = false;
		body.position = new Vector2(boundary.xMin, boundary.yMin);
	}
	
	void Update() {
		if (Input.GetKeyDown(KeyCode.S))
			startSimulation();
		interpreter.execute(startedSimulation);
	}
	
	public void moveSpaceship(Vector2 direction, float intensity) {
		body.velocity = direction * fixedSpeed * intensity;
		body.rotation = body.velocity.x * (-tilt);
		body.position = new Vector2 (Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax), 
		                             Mathf.Clamp(body.position.y, boundary.yMin, boundary.yMax));
	}

	public bool shoot() {
		if (Time.time > nextFire) {
			shootSound.Play();
			nextFire = Time.time + fireRate;
			GameObject newShot = GameObject.Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation) as GameObject;
			newShot.transform.parent = GameScreen.transform;
			return true;
		}
		else return false;
	}

	public void startSimulation() {
		if (!startedSimulation)
			startedSimulation = true;
	}
}
