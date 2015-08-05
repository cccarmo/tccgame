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

	public Vector3 IndexToPosition(int index) {
		int margin = 15, columns = 11, baseX = -220, baseY = 265;
		return new Vector3(baseX + margin + (index / columns) * 205,
		                   baseY + margin + (index % columns) * -55, 0);
	}


	public GameObject instantiateCommandBox(int index) {

		Command command = (Command) commandList[index];
		
		GameObject box = Instantiate(commandBoxPreFab) as GameObject;
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

	
	public void FixOrderOfBlock() {
		IComparer comparator = new YPosComparator ();
		commandsDrawn.Sort(comparator);

		commandList.Clear();

		int i = 0;
		foreach(var b in commandsDrawn) {
			GameObject box = (GameObject) b;
			CommandBox commandBox = box.GetComponent<CommandBox>();
			commandList.Add(commandBox.command);
			commandBox.SetLabelByIndex(i);
			commandBox.GoToPos(IndexToPosition(i));
			i++;
		}
	}

	public void Update() {
		FixOrderOfBlock ();
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

