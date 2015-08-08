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
	private bool startedSimulation, finishedAnimation, restart;

	public ArrayList commandsDrawn;
	
	private GUIStyle highlightStyle, ordinaryStyle;
	
	void Start() {
		commandList = new ArrayList();
		commandsDrawn = new ArrayList();
		restartSimulation();
	}
	
	
	public void restartSimulation() {
		nextCommandIndex = 0;
		startedSimulation = restart = false;
		finishedAnimation = true;
		DrawList();
	}

	public void saveCommandList() {
		restartSimulation();
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

	public Vector3 IndexToPosition(int index) {
		int margin = 15, columns = 11, baseX = -230, baseY = 230;
		return new Vector3(baseX + margin + (index / columns) * 205,
		                   baseY + margin + (index % columns) * -75, 0);
	}


	public GameObject instantiateCommandBox(int index) {

		Command command = (Command) commandList[index];
		
		GameObject box = Instantiate(command.getCommandBoxPreFab()) as GameObject;
		box.transform.SetParent(gameObject.transform);
		
		/// Place and fix local scale
		box.transform.localPosition = IndexToPosition(index);

		box.transform.localScale = new Vector2(1, 1);
		box.GetComponent<CommandBox>().Init(index, command);
		
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

	public void makeCommandListFromCommandsDrawn() {
		commandList.Clear();
		
		int i = 0;
		foreach(var b in commandsDrawn) {
			GameObject box = (GameObject) b;
			CommandBox commandBox = box.GetComponent<CommandBox>();
			commandList.Add(commandBox.command);
			commandBox.SetIndex(i);
			commandBox.GoToPos(IndexToPosition(i));
			i++;
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

