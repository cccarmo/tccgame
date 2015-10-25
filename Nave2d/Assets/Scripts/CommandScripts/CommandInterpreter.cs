using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;


public class CommandInterpreter : DataRetriever {
	private Command currentCommand;
	private ArrayList commandsDrawn;
	private int nextCommandIndex;
	private int maxCommands = 20;
	private bool startedSimulation, finishedAnimation, restart;
	private Stack scopedBeginning;
	public GameObject picker;
	public GameObject panelHelp;
	public SemanticInterpreter semanticInterpreter;

	private GameObject lastBox;

	
	void Start() {
		commandsDrawn = new ArrayList();
		scopedBeginning = new Stack();

		Queue persistentData = (Queue) retrieveData();
		if(persistentData != null) {
			semanticInterpreter = (SemanticInterpreter) persistentData.Dequeue();
			semanticInterpreter.setCommandInterpreter(this);
			ArrayList commandList = (ArrayList) persistentData.Dequeue();
			foreach(Command command in commandList)
				addCommand((Command) command);
		}
		else semanticInterpreter = new SemanticInterpreter(this);

		restartSimulation();
	}

	public void restartSimulation() {
		nextCommandIndex = 0;
		startedSimulation = restart = false;
		finishedAnimation = true;
	}

	public void saveCommandList() {
		restartSimulation();
		ArrayList commandList = getProgramFromPanel();
		Queue persistentData = new Queue();
		persistentData.Enqueue(semanticInterpreter);
		persistentData.Enqueue(commandList);
		saveData(persistentData);
	}

	public GameObject addComparison (GameObject eventType) {
		return instantiateComparisonBox(eventType); 
	}

	public GameObject addCommand(Command newCommand) {
		panelHelp.SetActive(false);
		newCommand.resetRepetitionCounter();
		if(commandsDrawn.Count < maxCommands && !startedSimulation) {
			int nestLevel = 0;
			ArrayList commandList = getProgramFromPanel();
			for(int index = 0; index < commandsDrawn.Count; index++) {
				Command command = (Command) commandList[index];
				
				if (command.indentLevel != 0)
					nestLevel = nestLevel + command.indentLevel;
			}

			GameObject box = instantiateCommandBox(newCommand, commandsDrawn.Count, nestLevel);
			if(newCommand.indentLevel == -1) {
				GameObject scopedBox = (GameObject) scopedBeginning.Pop();
				FlowCommandBox scopedCommand = scopedBox.GetComponent<FlowCommandBox>();
				scopedCommand.setEndOfScopeAsChild(box.GetComponent<CommandBox>());
			} else if(scopedBeginning.Count > 0) {
				GameObject scopedBox = (GameObject) scopedBeginning.Peek();
				box.transform.SetParent(scopedBox.transform);
				box.transform.localScale = new Vector2(1, 1);
			}
			if(newCommand.indentLevel == 1)
				scopedBeginning.Push(box);

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
			interpretCommand(getProgramFromPanel());
		}
	}
	
	public void startSimulation() {
		startedSimulation = true;
	}

	private Vector3 calculateBoxPosition(GameObject box, int index, int nestLevel) {
		int baseX = -310, baseY = 865, margin = 30;
		float width = box.GetComponent<RectTransform>().rect.width;
		return new Vector3(width/2 + baseX + margin + nestLevel * 50,
		                   baseY - margin + index * -60, 0);
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


	public ArrayList getProgramFromPanel() {
		IComparer comparator = new YPosComparator ();
		commandsDrawn.Sort(comparator);

		ArrayList commandList = new ArrayList();
		foreach(var b in commandsDrawn) {
			GameObject box = (GameObject) b;
			CommandBox commandBox = box.GetComponent<CommandBox>();
			Command command = commandBox.command;
			
			commandList.Add(command);
		}

		return commandList;
	}


	public void FixCommandsOrder() {
		int index = 0, nestLevel = 0;
		GameObject lastCommandBox = GameObject.FindGameObjectWithTag("DropPanel");

		foreach(var b in commandsDrawn) {
			GameObject box = (GameObject) b;
			CommandBox commandBox = box.GetComponent<CommandBox>();
			Command command = commandBox.command;

			if (command.indentLevel < 0)
				nestLevel = nestLevel + command.indentLevel;

			if (commandBox.isActive && !commandBox.pressed)
				commandBox.GoToPos(calculateBoxPosition(box, index, nestLevel));
			
			if (commandBox.pressed) {
				commandBox.transform.SetParent(lastCommandBox.transform);
				commandBox.transform.localScale = new Vector2(1, 1);
			}
			if (commandBox.GetComponent<FlowCommandBox>() != null) {
				lastCommandBox = commandBox.gameObject;
			} else {
				if (command.indentLevel == -1)
					lastCommandBox = lastCommandBox.transform.parent.gameObject;
			}
			
			if (command.indentLevel > 0)
				nestLevel = nestLevel + command.indentLevel;

			index++;
		}
	}

	
	public void FixOrderOfBlocks() {
		IComparer comparator = new YPosComparator ();
		commandsDrawn.Sort(comparator);
		FixCommandsOrder();
	}

	public void FixedUpdate() {
		FixOrderOfBlocks();
	}


	public void removeFromList(GameObject box){
		commandsDrawn.Remove(box);
		GameObject.Destroy(box);
		FixOrderOfBlocks();
	}


	public void SetMaxCommands(int newMax) {
		maxCommands = newMax;
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

