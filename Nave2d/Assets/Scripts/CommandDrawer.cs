using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandDrawer : MonoBehaviour {
	
	public LinkedList<Command> commandList; 
	
	public void Start() {
		
	}
	
	public void drawList(LinkedList<Command> commandList) {
		
	}
	
	public void OnGUI() {
		string label = "";
		
		commandList = transform.parent.GetComponent<CommandInterpreter>().commandList;
		
		int i, j;
		i = j = 0;
		foreach (Command command in commandList) {
			if (command.Equals (Command.Shoot))
				label = "Shoot!";
			else if (command.Equals(Command.TurnClockwise)) {
				label = "Turn Clockwise!";
			} else if (command.Equals(Command.TurnCounterclockwise)) {
				label = "Turn CounterClockwise!";
			} 
			else if (command.Equals(Command.GoForwards)) {
				label = "Go Forward!";
			}
			else if (command.Equals(Command.GoBackwards)) {
				label = "Go Backwards!";
			}
			else if (command.Equals(Command.GoLeftwards)) {
				label = "Go Leftward!";
			}
			else if (command.Equals(Command.GoRightwards)) {
				label = "Go Rightward!";
			}

			GUI.Box (new Rect (0 + j*105, i * 55, 100, 50), label);

			i++;
			if (i == 8) {
				i = 0;
				j++;
			}
		}
	}
}
