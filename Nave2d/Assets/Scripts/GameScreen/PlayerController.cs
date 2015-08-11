﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*[System.Serializable]
public class Boundary {
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
}*/

public class PlayerController : MonoBehaviour {

	enum AnimationType {
		moveToPlanet
	};

	private Rigidbody2D body;
	//public Boundary boundary;

	public readonly float fixedSpeed = 5.15f;
	public readonly float tilt = 3.0f;
	public readonly float fireRate = 0.25f;
	private int laserMissiles = 5;
	private float nextFire;
	public GameObject Shot;
	public Transform ShotSpawn;

	public GameObject GameScreen;
	public CommandInterpreter interpreter;
	private AudioSource shootSound;
	private AudioSource failSound;
	private AudioSource landingSound;

	private uint ticks;
	private readonly uint executionTime = 25;

	public Texture2D laserTexture;

	private bool interpretCommands;
	private bool animate;
	private Vector3 positionToMoveTo;
	private AnimationType currentAnimation;

	// Set this parameter always for the initial setup of the level
	public int direction = 0;

	// Boundarys
	private float xMin = 2.105591f;
	private float xMax = 11.90759f;
	private float yMin = -3.259508f;
	private float yMax = 8.675492f;

	// Step sizes
	private float dX, dY;

	private Vector2 nextPosition;


	void Start() {
		dX = (xMax - xMin) / 9f;
		dY = (yMax - yMin) / 11f;
		animate = false;
		interpretCommands = true;
		body = GetComponent<Rigidbody2D>();
		shootSound = GetComponents<AudioSource>()[0];
		failSound = GetComponents<AudioSource>()[1];
		landingSound = GetComponents<AudioSource> () [2];
		nextFire = 0.0f;
		ticks = 0;
	}
	
	void Update() {
		Debug.Log (body.position.x + " " + body.position.y);
		if (interpretCommands)
			interpreter.execute();
		if (animate) {
			switch (currentAnimation) {
				case AnimationType.moveToPlanet:
					animate = MoveToPosition(positionToMoveTo);
					break;
			}
		}
	}

	public bool animating() {
		return animate;
	}
	
	/*private bool move(Vector2 direction) {
		float intensity = ((float)executionTime - ticks - 1) / executionTime;
		body.velocity = direction * fixedSpeed * intensity;
		// Balancinho - tirei pq tava estragando o movimento depois que mudei pra ser relativo a rotacao
		//body.rotation = body.velocity.x * (-tilt);
		body.position = new Vector2 (Mathf.Clamp (body.position.x, boundary.xMin, boundary.xMax), 
	                             Mathf.Clamp (body.position.y, boundary.yMin, boundary.yMax));
		ticks = (ticks + 1) % executionTime;
		return (ticks == 0);
	}

	public bool moveForward() {
		float sin = Mathf.Sin (Mathf.Deg2Rad * (body.rotation - 90f));
		float cosin = Mathf.Cos (Mathf.Deg2Rad * (body.rotation - 90f));
		return move(new Vector2(-cosin, -sin));
	}

	public bool moveBackward() {
		float sin = Mathf.Sin (Mathf.Deg2Rad * (body.rotation - 90f));
		float cosin = Mathf.Cos (Mathf.Deg2Rad * (body.rotation - 90f));
		return move(new Vector2(cosin, sin));
	}

	public bool moveLeftwards() {
		float sin = Mathf.Sin (Mathf.Deg2Rad * (body.rotation - 90f));
		float cosin = Mathf.Cos (Mathf.Deg2Rad * (body.rotation - 90f));
		return move(new Vector2(sin, -cosin));
	}

	public bool moveRightwards() {
		float sin = Mathf.Sin (Mathf.Deg2Rad * (body.rotation - 90f));
		float cosin = Mathf.Cos (Mathf.Deg2Rad * (body.rotation - 90f));
		return move(new Vector2(-sin, cosin));
	}*/

	private bool moveToNextPosition() {
		Vector3 nextPlace = new Vector3 (nextPosition.x, nextPosition.y,0);
		body.position = Vector3.MoveTowards (body.position, nextPlace, 0.1f);
		ticks = (ticks + 1) % executionTime;
		if (ticks == 0) {
			body.position = Vector3.MoveTowards (body.position, nextPlace, 1f);
		}
		body.position = new Vector2 (Mathf.Clamp (body.position.x, xMin, xMax), 
		                             Mathf.Clamp (body.position.y, yMin, yMax));
		return (ticks == 0);
	}

	public bool moveForward() {
		if (ticks % executionTime == 0) {
			nextPosition = body.position;
			switch (direction) {
			case 0: nextPosition.y += dY;
				break;
			case 1: nextPosition.y += dY;
				nextPosition.x += dX;
				break;
			case 2: nextPosition.x += dX;
				break;
			case 3: nextPosition.y -= dY;
				nextPosition.x += dX;
				break;
			case 4: nextPosition.y -= dY;
				break;
			case 5: nextPosition.y -= dY;
				nextPosition.x -= dX;
				break;
			case 6: nextPosition.x -= dX;
				break;
			case 7: nextPosition.y += dY;
				nextPosition.x -= dX;
				break;
			}
		}
		return moveToNextPosition();
	}

	
	public bool moveBackward() {
		if (ticks % executionTime == 0) {
			nextPosition = body.position;
			switch (direction) {
			case 0: nextPosition.y -= dY;
				break;
			case 1: nextPosition.y -= dY;
				nextPosition.x -= dX;
				break;
			case 2: nextPosition.x -= dX;
				break;
			case 3: nextPosition.y += dY;
				nextPosition.x -= dX;
				break;
			case 4: nextPosition.y += dY;
				break;
			case 5: nextPosition.y += dY;
				nextPosition.x += dX;
				break;
			case 6: nextPosition.x += dX;
				break;
			case 7: nextPosition.y -= dY;
				nextPosition.x += dX;
				break;
			}
		}
		return moveToNextPosition();
	}
	
	public bool moveLeftwards() {
		if (ticks % executionTime == 0) {
			nextPosition = body.position;
			switch (direction) {
			case 0: nextPosition.x -= dX;
				break;
			case 1: nextPosition.y += dY;
				nextPosition.x -= dX;
				break;
			case 2: nextPosition.y += dY;
				break;
			case 3: nextPosition.y += dY;
				nextPosition.x += dX;
				break;
			case 4: nextPosition.x += dX;
				break;
			case 5: nextPosition.y -= dY;
				nextPosition.x += dX;
				break;
			case 6: nextPosition.y -= dY;
				break;
			case 7: nextPosition.y -= dY;
				nextPosition.x -= dX;
				break;
			}
		}
		return moveToNextPosition();
	}
	
	public bool moveRightwards() {
		if (ticks % executionTime == 0) {
			nextPosition = body.position;
			switch (direction) {
			case 0: nextPosition.x += dX;
				break;
			case 1: nextPosition.y -= dY;
				nextPosition.x += dX;
				break;
			case 2: nextPosition.y -= dY;
				break;
			case 3: nextPosition.y -= dY;
				nextPosition.x -= dX;
				break;
			case 4: nextPosition.x -= dX;
				break;
			case 5: nextPosition.y += dY;
				nextPosition.x -= dX;
				break;
			case 6: nextPosition.y += dY;
				break;
			case 7: nextPosition.y += dY;
				nextPosition.x += dX;
				break;
			}
		}
		return moveToNextPosition();
	}

	public bool turnClockwise() {
		if (ticks == 0) {
			direction = (direction + 1) % 8;
		}
		body.rotation -= 1.8f;
		ticks = (ticks + 1) % executionTime;
		return (ticks == 0);
	}

	public bool turnCounterClockwise() {
		if (ticks == 0) {
			direction = (direction - 1);
			if (direction == -1)
				direction = 7;
		}
		body.rotation += 1.8f;
		ticks = (ticks + 1) % executionTime;
		return (ticks == 0);
	}

	public bool shoot() {
		if (Time.time > nextFire && !failSound.isPlaying) {
			if (laserMissiles > 0) {
				laserMissiles--;
				shootSound.Play();
				nextFire = Time.time + fireRate;
				GameObject newShot = GameObject.Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation) as GameObject;
				newShot.transform.parent = GameScreen.transform;
			}
			else failSound.Play();

			return true;
		}
		else return false;
	}
  
	public void ArriveAtPlanet(Vector3 planetPosition) {
		interpretCommands = false;
		body.velocity = new Vector2(0,0);
		AudioPlayer.bgMusic.Stop();
		AudioPlayer.winMusic.Play();
		landingSound.Play();
		GetComponent<Animator> ().Play ("ArriveAtPlanet");
		// Set "moving to planet" animation
		positionToMoveTo = planetPosition;
		animate = true;
		currentAnimation = AnimationType.moveToPlanet;
		ticks = 1000;
	}
	
	public bool MoveToPosition (Vector3 position) {
		if (ticks == 1)
			body.position = Vector3.MoveTowards (body.position, position, 1f);
		else 
			body.position = Vector3.MoveTowards (body.position, position, .01f);
		ticks--;
		return ticks > 0;
	}

  	void OnGUI() {   
		GUIStyle style = new GUIStyle();
		style.fontSize = 20;
		style.normal.textColor = Color.white;
		
		Rect missiles = new Rect(0.95f*Screen.width, 0.73f*Screen.height, 16, 16);
		GUI.Label(missiles, laserMissiles.ToString(), style);
		Rect laserIconSpace = new Rect(missiles.xMax, 0.5f*missiles.yMin + 0.5f*missiles.yMax, 20, 5);
		GUI.DrawTexture(laserIconSpace, laserTexture);                      
	}
}
