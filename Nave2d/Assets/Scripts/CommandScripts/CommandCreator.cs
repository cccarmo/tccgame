using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


delegate Command newCommandClosure();


public class CommandCreator : MonoBehaviour {
	public GameObject spaceShip;
	private CommandInterpreter interpreter;
	public GameObject panel;
	private Dictionary<string, newCommandClosure> actions;
	public GameObject[] availableBoxes;
	
	
	private bool KFunctionTrue() {
		return true;
	}
	
	private bool KFlowFunctionTrue(BoolCondition cond, ref int programCounter) {
		return true;
	}
	
	void Start() {
		PlayerController spaceShipController = spaceShip.GetComponent<PlayerController>();
		interpreter = panel.GetComponent<CommandInterpreter>();
		
		actions = new Dictionary<string, newCommandClosure>();
		
		// Creating Command Generators
		newCommandClosure newShootCommand = () => new Command (spaceShipController.shoot, null, "Shoot", availableBoxes [0], false, 0);
		newCommandClosure newFowardCommand = () => new Command (spaceShipController.moveForward, null, "Move Forward", availableBoxes [1], false ,0);
		newCommandClosure newBackwardCommand = () => new Command (spaceShipController.moveBackward, null, "Move Backward", availableBoxes [2], false, 0);
		newCommandClosure neweLeftwardCommand = () => new Command (spaceShipController.moveLeftwards, null, "Move Leftwards", availableBoxes [3], false, 0);
		newCommandClosure newRightwardCommand = () => new Command(spaceShipController.moveRightwards, null, "Move Rightwards", availableBoxes[4], false, 0);
		newCommandClosure newClockwiseCommand = () => new Command(spaceShipController.turnClockwise, null, "Turn Clockwise", availableBoxes[5], false, 0);
		newCommandClosure newCounterClockwiseCommand = () => new Command(spaceShipController.turnCounterClockwise, null, "Turn Counterclockwise", availableBoxes[6], false, 0);
		newCommandClosure newForCommand = () => new Command(null, interpreter.semanticInterpreter.ForCommand, "Scoped Repetition", availableBoxes[7], true, 1);
		newCommandClosure newEndForCommand = () => new Command(null, interpreter.semanticInterpreter.EndForCommand, "Scoped Repetition End", availableBoxes[8], true, -1);
		
		
		// Adding Ship Commands to dictionary
		actions.Add("Shoot", newShootCommand);
		actions.Add("Move Forward", newFowardCommand);
		actions.Add("Move Backward", newBackwardCommand);
		actions.Add("Move Leftwards", neweLeftwardCommand);
		actions.Add("Move Rightwards", newRightwardCommand);
		actions.Add("Turn Clockwise", newClockwiseCommand);
		actions.Add("Turn Counterclockwise", newCounterClockwiseCommand);
		
		// Adding Flow Commands to dictionary
		actions.Add("Scoped Repetition", newForCommand);
		actions.Add("Scoped Repetition End", newEndForCommand);
	}
	
	public void handleEvent(string eventType) {
		Rect panelRect = panel.transform.GetComponent<RectTransform>().rect;
		Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
		
		if (panelRect.Contains (mousePos)) {
			interpreter.addCommand ((Command) (actions [eventType])());
			if (eventType == "Scoped Repetition") {
				interpreter.addCommand ((Command) (actions ["Scoped Repetition End"])());
			}
		}
	}
}
