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

	private uint ticks;
	private readonly uint executionTime = 25;

	void Start() {
		body = GetComponent<Rigidbody2D>();
		shootSound = GetComponent<AudioSource>();
		nextFire = 0.0f;
		ticks = 0;
		body.position = new Vector2(boundary.xMin, boundary.yMin);
	}
	
	void Update() {
		interpreter.execute();
	}
	
	private bool moveSpaceship(Vector2 direction) {
		float intensity = ((float) executionTime - ticks - 1)/executionTime;
		body.velocity = direction * fixedSpeed * intensity;
		// Balancinho - tirei pq tava estragando o movimento depois que mudei pra ser relativo a rotacao
		//body.rotation = body.velocity.x * (-tilt);
		body.position = new Vector2 (Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax), 
		                             Mathf.Clamp(body.position.y, boundary.yMin, boundary.yMax));
		ticks = (ticks + 1) % executionTime;
		return (ticks == 0);
	}

	public bool moveSpaceshipForward() {
		float sin = Mathf.Sin (Mathf.Deg2Rad *(body.rotation - 90f));
		float cosin = Mathf.Cos (Mathf.Deg2Rad * (body.rotation - 90f));
		return moveSpaceship(new Vector2(-cosin, -sin));
	}

	public bool moveSpaceshipBackward() {
		float sin = Mathf.Sin (Mathf.Deg2Rad *(body.rotation - 90f));
		float cosin = Mathf.Cos (Mathf.Deg2Rad * (body.rotation - 90f));
		return moveSpaceship(new Vector2(cosin, sin));
	}

	public bool moveSpaceshipLeftwards() {
		float sin = Mathf.Sin (Mathf.Deg2Rad *(body.rotation - 90f));
		float cosin = Mathf.Cos (Mathf.Deg2Rad * (body.rotation - 90f));
		return moveSpaceship(new Vector2(sin, -cosin));
	}

	public bool moveSpaceshipRightwards() {
		float sin = Mathf.Sin (Mathf.Deg2Rad *(body.rotation - 90f));
		float cosin = Mathf.Cos (Mathf.Deg2Rad * (body.rotation - 90f));
		return moveSpaceship(new Vector2(-sin, cosin));
	}

	public bool turnSpaceship(TurnDirection direction) {
		switch (direction) {
			case TurnDirection.Clockwise:
				body.rotation -= 1.8f;
				break;
			case TurnDirection.Counterclockwise:
				body.rotation += 1.8f;
				break;
		}
		ticks = (ticks + 1) % executionTime;
		return (ticks == 0);
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
}
