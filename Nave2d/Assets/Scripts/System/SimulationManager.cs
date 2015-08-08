using UnityEngine;
using System.Collections;

public class SimulationManager : MonoBehaviour {
	public PlayerController spaceship;
	public CommandInterpreter interpreter;
	private GameController gameController;

	private bool running;
	public Enduring[] lastingObjects;
	
	void Start() {
		running = false;
		gameController = gameObject.GetComponentInParent<GameController>();
	}
	
	void Update() {
		if(running && interpreter.shouldRestartSimulation()) {
			simulate();
		}
	}
	
	public void simulate() {
		if(!running) {
			interpreter.startSimulation();
			running = true;
		}
		else if(!spaceship.animating()) {
			// interpreter.saveCommandList();
			StartCoroutine(gameController.sleepFor(2.5f));
      		Application.LoadLevel(Application.loadedLevel);
		}
	}
	
	public void restart() {
		foreach(Enduring item in lastingObjects) {
			item.restartInReload();
		}
		Application.LoadLevel(Application.loadedLevel);
	}

	public bool simulationRunning() {
		return running;
	}
}
