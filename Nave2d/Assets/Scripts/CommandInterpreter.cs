using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;


delegate bool Command();

public class CommandInterpreter : MonoBehaviour {
	private Command currentCommand;
	public ArrayList commandList;
	private int nextCommandIndex;
	private readonly int maxCommands = 21;
	private bool startedSimulation, finishedAnimation;
	
	public GameObject spaceShip;
	private PlayerController spaceShipController;
	
	public GameObject commandBoxPreFab;
	public ArrayList commandsDrawn;
	
	private GUIStyle highlightStyle, ordinaryStyle;
	
	
	void Start() {
		spaceShipController = spaceShip.GetComponent<PlayerController>();
		commandList = new ArrayList();
		commandsDrawn = new ArrayList();
		resetSimulation();
	}
	
	public void AddShootCommand() {
		Debug.Log("AQUI");
		addCommand(new Command(spaceShipController.shoot));
	}
	
	public void addMoveForwardsCommand() {
		addCommand(new Command(spaceShipController.moveForward));
	}
	
	public void addMoveBackwardsCommand() {
		addCommand(new Command(spaceShipController.moveBackward));
	}
	
	public void addMoveLeftwardsCommand() {
		addCommand(new Command(spaceShipController.moveLeftwards));
	}
	
	public void addMoveRightwardsCommand() {
		addCommand(new Command(spaceShipController.moveRightwards));
	}
	
	public void addTurnClockwiseCommand() {
		addCommand(new Command(spaceShipController.turnClockwise));
	}
	
	public void addTurnCounterClockwiseCommand() {
		addCommand(new Command(spaceShipController.turnCounterClockwise));
	}
	
	private void resetSimulation() {
		nextCommandIndex = 0;
		startedSimulation = false;
		finishedAnimation = true;
		CleanListDraw();
	}
	
	private void addCommand(Command command) {
		if(commandList.Count < maxCommands && !startedSimulation) {
			commandList.Add(command);
			CleanListDraw();
			DrawList();
		}
	}
	
	private void interpretCommand() {
		try {
			if (finishedAnimation)
				currentCommand = getNextCommand ();
			finishedAnimation = currentCommand ();
		} catch (IndexOutOfRangeException) {
			resetSimulation (); // restart stage as well
		}
	}
	
	private Command getNextCommand() {
		if(nextCommandIndex < commandList.Count) {
			Command nextCommand = (Command) commandList[nextCommandIndex];
			nextCommandIndex++;
			return nextCommand;
		}
		else throw new IndexOutOfRangeException();
	}
	
	public void execute() {
		if(startedSimulation)
			interpretCommand();
	}
	
	public void startSimulation() {
		if(!startedSimulation)
			startedSimulation = true;
		else {
			startedSimulation = false;
			resetSimulation(); // restart stage as well
		}
	}
	
	
	/// List Drawing Methods
	public void CleanListDraw() {
		foreach(var commandBox in commandsDrawn) {
			GameObject box = (GameObject) commandBox;
			GameObject.Destroy(box);
		}
	}
	
	public void DrawList() {
		Command command;
		string label = "";
		int highlight = nextCommandIndex - 1, margin = 15, columns = 3;
		int baseX = -220, baseY = 265;
		
		for(int index = 0; index < commandList.Count; index++) {
			command = (Command) commandList[index];
			label = command.Method.ToString();
			GameObject box;

			/// Decide later how to proceed with highlight.  Maybe use different pre-fabs, or change base collor.
			if(index == highlight) {
				box = Instantiate(commandBoxPreFab) as GameObject;
				box.transform.SetParent(gameObject.transform);
			}
			else {
				box = Instantiate(commandBoxPreFab) as GameObject;
				box.transform.SetParent(gameObject.transform);
			}

			/// Place and fix local scale
			box.transform.localPosition = new Vector3(baseX + margin + (index % columns) * 205, baseY + margin + (index / columns) * -55, 0);
			box.transform.localScale = new Vector2(1, 1);

			box.name = "Command " + index + " " + label;

			CommandBox commandBox = box.GetComponent<CommandBox>() as CommandBox;
			commandBox.Init(label);

			commandsDrawn.Add(box);
		}
	}
}