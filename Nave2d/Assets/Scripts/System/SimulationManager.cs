using UnityEngine;
using System.Collections;

public class SimulationManager : Scheduler {
	private PlayerController spaceship;
	public CommandInterpreter interpreter;

	private bool running;
	
	void Start() {
		running = false;
		spaceship = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
	}
	
	void Update() {
		if(running && interpreter.shouldRestartSimulation()) {
			interpreter.saveCommandList();
			executeAfter(1.25f, new Action(reloadLevel));
		}
	}
	
	public void simulate() {
		if(!running) {
			interpreter.startSimulation();
			running = true;
		}
		else if(!spaceship.animating()) {
			reloadLevel();
		}
	}

	public void reloadLevel() {
		interpreter.saveCommandList();
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void restart() {
		Enduring[] lastingObjects = Enduring.getLastingObjects();
		foreach(Enduring item in lastingObjects) {
			item.restartInReload();
		}
		Application.LoadLevel(Application.loadedLevel);
	}

	public bool simulationRunning() {
		return running;
	}
}
