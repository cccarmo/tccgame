using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


delegate Command newCommandClosure();


public class CommandCreator : MonoBehaviour {
	private GameObject spaceShip;
	private CommandInterpreter interpreter;
	public GameObject panel;
	private Dictionary<string, newCommandClosure> actions;
	private Dictionary<string, GameObject> comparisons;
	public GameObject[] availableBoxes;
	
	
	private bool KFunctionTrue() {
		return true;
	}
	
	private bool KFlowFunctionTrue(BoolCondition cond, ref int programCounter) {
		return true;
	}
	
	void Start() {
		spaceShip = GameObject.FindWithTag ("Player");
		PlayerController spaceShipController = spaceShip.GetComponent<PlayerController>();
		interpreter = panel.GetComponent<CommandInterpreter>();
		
		actions = new Dictionary<string, newCommandClosure>();
		comparisons = new Dictionary<string,  GameObject>();
		
		// Creating Command Generators
		newCommandClosure newShootCommand = () => new ShipCommand (spaceShipController.shoot, "Shoot", availableBoxes [0]);
		newCommandClosure newFowardCommand = () => new ShipCommand (spaceShipController.moveForward, "Move Forward", availableBoxes [1]);
		newCommandClosure newBackwardCommand = () => new ShipCommand (spaceShipController.moveBackward, "Move Backward", availableBoxes [2]);
		newCommandClosure neweLeftwardCommand = () => new ShipCommand (spaceShipController.moveLeftwards, "Move Leftwards", availableBoxes [3]);
		newCommandClosure newRightwardCommand = () => new ShipCommand(spaceShipController.moveRightwards, "Move Rightwards", availableBoxes[4]);
		newCommandClosure newClockwiseCommand = () => new ShipCommand(spaceShipController.turnClockwise, "Turn Clockwise", availableBoxes[5]);
		newCommandClosure newCounterClockwiseCommand = () => new ShipCommand(spaceShipController.turnCounterClockwise, "Turn Counterclockwise", availableBoxes[6]);

		newCommandClosure newForCommand = () => new FlowCommand(interpreter.semanticInterpreter.ForCommand, "Scoped Repetition", availableBoxes[7], 1);
		newCommandClosure newEndForCommand = () => new FlowCommand(interpreter.semanticInterpreter.EndForCommand, "Scoped Repetition End", availableBoxes[8], -1);
		newCommandClosure newForComparisonCommand = () => new FlowCommand(interpreter.semanticInterpreter.ForCommand, "Scoped Repetition", availableBoxes[9], 1);

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
		actions.Add("Scoped Repetition Comparison", newForComparisonCommand);

		// Adding Comparison boxes to dictionary
		comparisons.Add ("Comparing Asteroid Number", availableBoxes [10]);

	}
	
	public GameObject handleEvent(string eventType) {
		GameObject box;
		if (eventType.Contains ("Comparing")) {
			box = interpreter.addComparison (comparisons[eventType]);
		} else {
			box = interpreter.addCommand((Command) (actions [eventType])());
			if (eventType.Contains("Scoped Repetition"))
				interpreter.addCommand ((Command)(actions ["Scoped Repetition End"])());
		}
		return box;
	}

	public void rebuildCommands(ArrayList commandsList) {
		foreach(Command eventType in commandsList)
			interpreter.addCommand((Command) (actions [eventType.ToString()])());
	}
}
