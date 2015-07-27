using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

delegate bool Command();

public class CommandInterpreter : MonoBehaviour {
	private Command currentCommand;
	public ArrayList commandList;
	private int nextCommandIndex;
	private readonly int maxCommands = 21;
	private bool startedSimulation, finishedAnimation;
	
	public GameObject spaceShip;
	private PlayerController spaceShipController;

	private GUIStyle highlightStyle, ordinaryStyle;


	void Start() {
		spaceShipController = spaceShip.GetComponent<PlayerController>();
		commandList = new ArrayList();
		resetSimulation();
	}

	public void addShootCommand() {
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
	}

	private void addCommand(Command command) {
		if(commandList.Count < maxCommands && !startedSimulation)
			commandList.Add(command);
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

	private void defineGUIStyles() {
		if(highlightStyle == null && ordinaryStyle == null) {
			highlightStyle = new GUIStyle(GUI.skin.button);
			highlightStyle.alignment = TextAnchor.MiddleCenter;
			highlightStyle.hover = highlightStyle.normal;
			ordinaryStyle = new GUIStyle(GUI.skin.box);
			ordinaryStyle.alignment = TextAnchor.MiddleCenter;
		}
	}

	public void OnGUI() {
		Command command;
		string label = "";
		int highlight = nextCommandIndex - 1, margin = 15, columns = 3;

		defineGUIStyles();
		for(int index = 0; index < commandList.Count; index++) {
			command = (Command) commandList[index];
			label = command.Method.ToString();

			// Maybe we should use windows instead to implement removal: GUI.Window(id, Rect, function, label);
			if(index == highlight)
				GUI.Box(new Rect(margin + (index % columns) * 105, margin + (index / columns) * 55, 100, 50), label, highlightStyle);
			else
				GUI.Box(new Rect(margin + (index % columns) * 105, margin + (index / columns) * 55, 100, 50), label, ordinaryStyle);
		}
	}
}