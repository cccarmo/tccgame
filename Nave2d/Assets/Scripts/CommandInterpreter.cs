using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;

public class CommandInterpreter : MonoBehaviour {
	private Command currentCommand;
	public ArrayList commandList;
	private int nextCommandIndex;
	private readonly int maxCommands = 21;
	private bool startedSimulation, finishedAnimation;
	
	public GameObject commandBoxPreFab;
	public ArrayList commandsDrawn;
	
	private GUIStyle highlightStyle, ordinaryStyle;

	void Start() {
		commandList = new ArrayList();
		commandsDrawn = new ArrayList();
		resetSimulation();
	}


	private void resetSimulation() {
		nextCommandIndex = 0;
		startedSimulation = false;
		finishedAnimation = true;
		DrawList();
	}
	
	public void addCommand(Command command) {
		if(commandList.Count < maxCommands && !startedSimulation) {
			commandList.Add(command);
			DrawList();
		}
	}
	
	private void interpretCommand() {
		try {
			if (finishedAnimation) {
				currentCommand = getNextCommand();

				DrawList();
				GameObject box = (GameObject) commandsDrawn[nextCommandIndex-1];
				box.GetComponent<CommandBox>().Highlight();
			}

			finishedAnimation = currentCommand.execute();
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

	public GameObject instantiateCommandBox(int index) {
		int margin = 15, columns = 11, baseX = -220, baseY = 265;
		Command command = (Command) commandList[index];

		GameObject box = Instantiate(commandBoxPreFab) as GameObject;
		box.transform.SetParent(gameObject.transform);

		/// Place and fix local scale
		box.transform.localPosition = new Vector3(baseX + margin + (index / columns) * 205,
		                                          baseY + margin + (index % columns) * -55, 0);
		box.transform.localScale = new Vector2(1, 1);
		box.GetComponent<CommandBox>().setLabelBox(index + " " + command.ToString());

		return box;
	}
	
	public void DrawList() {
		foreach(var commandBox in commandsDrawn) {
			GameObject box = (GameObject) commandBox;
			GameObject.Destroy(box);
		}
		commandsDrawn.Clear();

		for(int index = 0; index < commandList.Count; index++) {
			GameObject box = instantiateCommandBox(index);
			commandsDrawn.Add(box);
		}
	}
}