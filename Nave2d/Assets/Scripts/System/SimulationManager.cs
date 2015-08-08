using UnityEngine;
using System.Collections;

public class SimulationManager : Scheduler {
	public PlayerController spaceship;
	public CommandInterpreter interpreter;

	private bool running;
	public Enduring[] lastingObjects;
	
	void Start() {
		running = false;
	}
	
	void Update() {
		if(running && interpreter.shouldRestartSimulation()) {
			interpreter.saveCommandList();
			executeAfter(1.5f, new Action(reloadLevel));
		}
	}
	
	public void simulate() {
		if(!running) {
			interpreter.startSimulation();
			running = true;
		}
		else if(!spaceship.animating()) {
			interpreter.saveCommandList();
			reloadLevel();
		}
	}

	public void reloadLevel() {
		Application.LoadLevel(Application.loadedLevel);
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
