using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;


public class CommandInterpreter : DataRetriever {
	private Command currentCommand;
	public ArrayList commandList;
	private ArrayList commandsDrawn;
	private int nextCommandIndex;
	private readonly int maxCommands = 21;
	private bool startedSimulation, finishedAnimation, restart;
	public GameObject picker;
	public SemanticInterpreter semanticInterpreter;

	public GameObject lastBox;

	
	void Start() {
		commandList = new ArrayList();
		commandsDrawn = new ArrayList();
		semanticInterpreter = new SemanticInterpreter();
		semanticInterpreter.setCommandInterpreter(this);
		restartSimulation();

		ArrayList persistedList = (ArrayList) retrieveData();
		if(persistedList != null) {
			CommandCreator creator = picker.GetComponent<CommandCreator>();
			creator.buildCommandListByName(persistedList);
		}
	}

	public void restartSimulation() {
		nextCommandIndex = 0;
		startedSimulation = restart = false;
		finishedAnimation = true;
		DrawList();
	}

	public void saveCommandList() {
		restartSimulation();
		ArrayList commandNames = new ArrayList();
		foreach(Command command in commandList)
			commandNames.Add(command.ToString());
		saveData(commandNames);
	}

	public void addCommand(Command command) {
		if(commandList.Count < maxCommands && !startedSimulation) {
			commandList.Add(command);
			DrawList();   /// TODO Remove this line after dani's command 
		}
	}
	
	private void interpretCommand() {
		try {
			if (finishedAnimation) {
				currentCommand = getNextCommand();
				
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

	private Command getNextCommand() {
		if(nextCommandIndex < commandList.Count) {
			Command nextCommand = (Command) commandList[nextCommandIndex];
			nextCommandIndex++;
			return nextCommand;
		}
		else throw new IndexOutOfRangeException();
	}
	
	public void execute() {
		if(startedSimulation) {
			interpretCommand();
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


	public GameObject instantiateCommandBox(int index, int nestLevel) {
		Command command = (Command) commandList[index];
		
		GameObject box = Instantiate(command.getCommandBoxPreFab()) as GameObject;
		box.transform.SetParent(gameObject.transform);
		
		/// Place and fix local scale
		box.transform.localPosition = calculateBoxPosition(box, index, nestLevel);
		box.transform.localScale = new Vector2(1, 1);
		box.GetComponent<CommandBox>().Init(command);
		
		return box;
	}
	
	public void DrawList() {
		foreach(var commandBox in commandsDrawn) {
			GameObject box = (GameObject) commandBox;
			GameObject.Destroy(box);
		}
		commandsDrawn.Clear();

		int nestLevel = 0;
		for(int index = 0; index < commandList.Count; index++) {
			Command c = (Command) commandList[index];

			if (c.indentLevel < 0)
				nestLevel = nestLevel + c.indentLevel;

			GameObject box = instantiateCommandBox(index, nestLevel);

			if (c.indentLevel > 0)
				nestLevel = nestLevel + c.indentLevel;

			commandsDrawn.Add(box);
		}
	}

	public void makeCommandListFromCommandsDrawn() {
		commandList.Clear();
		
		int index = 0, nestLevel = 0;
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

