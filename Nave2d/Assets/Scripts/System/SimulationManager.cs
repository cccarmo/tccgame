using UnityEngine;
using System.Collections;

public class SimulationManager : Scheduler {
	private PlayerController spaceship;
	public CommandInterpreter interpreter;

	private bool running;
	
	void Start() {
		running = false;
	}
	
	void Update() {
		if (spaceship == null) {
			spaceship = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
		}
		if(running && interpreter.shouldRestartSimulation()) {
			interpreter.saveCommandList();
			executeAfter(1.25f, new Action(reloadLevel));
		}
	}
	
	public void simulate() {

		bool containsIncompleteComparisons = false;
		GameObject[] comparisonFlowBoxes = GameObject.FindGameObjectsWithTag ("ComparisonFlow");
		FlowCommandComparisonBox FlowComparisonBox;
		foreach (GameObject comparisonFlowBox in comparisonFlowBoxes) {
			FlowComparisonBox = comparisonFlowBox.gameObject.GetComponent<FlowCommandComparisonBox>();
			if (!FlowComparisonBox.isComplete) {
				containsIncompleteComparisons = true;
				break;
			}
		}

		// Add some feedback for the user here
		if (containsIncompleteComparisons) {
			Debug.Log("Falta completar alguma comparação");
			return;
		}

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
