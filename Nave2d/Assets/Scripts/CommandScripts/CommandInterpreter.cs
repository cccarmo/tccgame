using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;


public class CommandInterpreter : DataRetriever {
	private Command currentCommand;
	private ArrayList commandsDrawn;
	private int nextCommandIndex;
	private readonly int maxCommands = 21;
	private bool startedSimulation, finishedAnimation, restart;
	public GameObject picker;
	public SemanticInterpreter semanticInterpreter;

	public GameObject lastBox;

	
	void Start() {
		commandsDrawn = new ArrayList();
		semanticInterpreter = new SemanticInterpreter(this);
		restartSimulation();

		ArrayList persistedList = (ArrayList) retrieveData();
		if(persistedList != null) {
			CommandCreator creator = picker.GetComponent<CommandCreator>();
			creator.rebuildCommands(persistedList);
		}
	}

	public void restartSimulation() {
		nextCommandIndex = 0;
		startedSimulation = restart = false;
		finishedAnimation = true;
	}

	public void saveCommandList() {
		restartSimulation();
		ArrayList commandList = makeCommandListFromCommandsDrawn();
		saveData(commandList);
	}

	public GameObject addComparison (GameObject eventType) {
		return instantiateComparisonBox(eventType); 
	}

	public GameObject addCommand(Command newCommand) {
		if(commandsDrawn.Count < maxCommands && !startedSimulation) {
			int nestLevel = 0;
			ArrayList commandList = makeCommandListFromCommandsDrawn();
			for(int index = 0; index < commandsDrawn.Count; index++) {
				Command command = (Command) commandList[index];
				
				if (command.indentLevel < 0)
					nestLevel = nestLevel + command.indentLevel;
				else if (command.indentLevel > 0)
					nestLevel = nestLevel + command.indentLevel;
			}

			GameObject box = instantiateCommandBox(newCommand, commandsDrawn.Count, nestLevel);
			commandsDrawn.Add(box);

			return box;
		}

		return null;
	}

	private void interpretCommand(ArrayList commandList) {
		try {
			if (finishedAnimation) {
				currentCommand = getNextCommand(commandList);
				
				if (lastBox)
					lastBox.GetComponent<CommandBox>().NormalHighlight();

				GameObject box = (GameObject) commandsDrawn[nextCommandIndex-1];
				lastBox = box;
				box.GetComponent<CommandBox>().Highlight();
			}
			
			finishedAnimation = currentCommand.execute(ref nextCommandIndex);
		} catch (IndexOutOfRangeException) {
			restart = true;
		}
	}

	public bool shouldRestartSimulation() {
		return restart;
	}

	private Command getNextCommand(ArrayList commandList) {
		if(nextCommandIndex < commandList.Count) {
			Command nextCommand = (Command) commandList[nextCommandIndex];
			nextCommandIndex++;
			return nextCommand;
		}
		else throw new IndexOutOfRangeException();
	}
	
	public void execute() {
		if(startedSimulation) {
			interpretCommand(makeCommandListFromCommandsDrawn());
		}
	}
	
	public void startSimulation() {
		startedSimulation = true;
	}

	private Vector3 calculateBoxPosition(GameObject box, int index, int nestLevel) {
		int baseX = -310, baseY = 275, margin = 30;
		float width = box.GetComponent<RectTransform>().rect.width;
		return new Vector3(width/2 + baseX + margin + nestLevel * 50,
		                   baseY - margin + index * -55, 0);
	}


	private GameObject instantiateCommandBox(Command command, int index, int nestLevel) {
		GameObject box = Instantiate(command.getCommandBoxPreFab()) as GameObject;
		box.transform.SetParent(gameObject.transform);
		
		/// Place and fix local scale
		box.transform.localPosition = calculateBoxPosition(box, index, nestLevel);
		box.transform.localScale = new Vector2(1, 1);
		box.GetComponent<CommandBox>().Init(command);
		
		return box;
	}

	private GameObject instantiateComparisonBox(GameObject boxType) {
		GameObject box = Instantiate(boxType);
		box.transform.SetParent(gameObject.transform);
		
		/// Place and fix local scale
		//box.transform.localPosition = calculateBoxPosition(box, index, nestLevel);
		box.transform.localScale = new Vector2(1, 1);

		return box;
	}

	public ArrayList makeCommandListFromCommandsDrawn() {
		int index = 0, nestLevel = 0;
		ArrayList commandList = new ArrayList();

		foreach(var b in commandsDrawn) {
			GameObject box = (GameObject) b;
			CommandBox commandBox = box.GetComponent<CommandBox>();
			Command command = commandBox.command;

			commandList.Add(command);
			if (command.indentLevel < 0)
				nestLevel = nestLevel + command.indentLevel;

			commandBox.GoToPos(calculateBoxPosition(box, index, nestLevel));
			
			if (command.indentLevel > 0)
				nestLevel = nestLevel + command.indentLevel;

			index++;
		}
		return commandList;
	}

	
	public void FixOrderOfBlock() {
		IComparer comparator = new YPosComparator ();
		commandsDrawn.Sort(comparator);
		makeCommandListFromCommandsDrawn();
	}

	public void Update() {
		FixOrderOfBlock();
	}


	public void removeFromList(GameObject box){
		commandsDrawn.Remove(box);
		GameObject.Destroy(box);
		makeCommandListFromCommandsDrawn();
	}
}


public class YPosComparator : IComparer {
	int IComparer.Compare(System.Object x, System.Object y)  {
		GameObject a = (GameObject)x;
		GameObject b = (GameObject)y;

		if (b == null) return 1;
		if (a == null) return -1;
		if (a.transform.position.y > b.transform.position.y)
			return -1;
		else if (a.transform.position.y == b.transform.position.y)
			return 0;
		else 
			return 1;
	}
}

