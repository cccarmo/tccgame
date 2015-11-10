using UnityEngine;
using System.Collections;

public class SimulationManager : Scheduler {
	private PlayerController spaceship;
	private CommandInterpreter interpreter;

	private bool running;
	private bool foundSpaceship = false;
	public GameObject Player;
	
	void Start() {
		GameObject panel = GameObject.FindWithTag ("DropPanel");
		interpreter = panel.GetComponent<CommandInterpreter>();

		running = false;
	}
	
	void Update() {
		if (!foundSpaceship) {
			if (GameObject.FindWithTag("Player") != null) {
				spaceship = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
				foundSpaceship = true;
			}
		}
		if(running && interpreter.shouldRestartSimulation()) {
			interpreter.saveCommandList();
			executeAfter(1.75f, new Action(reloadLevel));
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

		if (!foundSpaceship) {
			GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
			int rand = Random.Range(0, spawners.Length);
			GameObject spaceshipGO = GameObject.Instantiate(Player, spawners[rand].transform.position, spawners[rand].transform.rotation) as GameObject;
			spaceshipGO.transform.parent = spawners[rand].transform.parent;
			spaceship = spaceshipGO.GetComponent<PlayerController>();
			foreach (GameObject spawner in spawners) {
				Destroy(spawner);
			}
			foundSpaceship = true;
		}

		if(!running) {
			executeAfter(0.5f, new Action(interpreter.startSimulation));
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
