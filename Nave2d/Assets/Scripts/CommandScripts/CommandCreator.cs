using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

delegate Command newCommandClosure();
delegate Comparison newComparisonClosure();

public class CommandCreator : MonoBehaviour {
	private CommandInterpreter interpreter;
	private Dictionary<string, newCommandClosure> actions;
	private Dictionary<string, newComparisonClosure> comparisons;
	public GameObject[] availableBoxes;

	private bool KFunctionTrue() {
		return true;
	}
	
	private bool KFlowFunctionTrue(BoolCondition cond, ref int programCounter) {
		return true;
	}

	void Start() {
		GameObject panel = GameObject.FindWithTag ("DropPanel");
		interpreter = panel.GetComponent<CommandInterpreter>();
		
		actions = new Dictionary<string, newCommandClosure>();
		comparisons = new Dictionary<string, newComparisonClosure>();
		
		// Creating Command Generators
		newCommandClosure newShootCommand = () => new ShootCommand(this);
		newCommandClosure newShieldCommand = () => new ShieldCommand(this);
		newCommandClosure newFowardCommand = () => new MoveForwardCommand(this);
		newCommandClosure newBackwardCommand = () => new MoveBackwardCommand(this);
		newCommandClosure neweLeftwardCommand = () => new MoveLeftwardsCommand(this);
		newCommandClosure newRightwardCommand = () => new MoveRightwardsCommand(this);
		newCommandClosure newClockwiseCommand = () => new TurnClockwiseCommand(this);
		newCommandClosure newCounterclockwiseCommand = () => new TurnCounterclockwiseCommand(this);

		newCommandClosure newForCommand = () => new FlowCommand(interpreter.semanticInterpreter.ForCommand, "Scoped Repetition", availableBoxes[7], 1, true);
		newCommandClosure newEndForCommand = () => new FlowCommand(interpreter.semanticInterpreter.EndForCommand, "Scoped Repetition End", availableBoxes[8], -1, false);
		newCommandClosure newForComparisonCommand = () => new FlowCommand(interpreter.semanticInterpreter.ForCommand, "Scoped Repetition", availableBoxes[9], 1, true);
		newCommandClosure newIfCommand = () => new FlowCommand(interpreter.semanticInterpreter.ForCommand, "Scoped Repetition", availableBoxes[11], 1, false);
		
		newComparisonClosure newComparison = () => new Comparison(availableBoxes[10]);

		// Adding Ship Commands to dictionary
		actions.Add("Shoot", newShootCommand);
		actions.Add("Shield", newShieldCommand);
		actions.Add("Move Forward", newFowardCommand);
		actions.Add("Move Backward", newBackwardCommand);
		actions.Add("Move Leftwards", neweLeftwardCommand);
		actions.Add("Move Rightwards", newRightwardCommand);
		actions.Add("Turn Clockwise", newClockwiseCommand);
		actions.Add("Turn Counterclockwise", newCounterclockwiseCommand);
		
		// Adding Flow Commands to dictionary
		actions.Add("Scoped Repetition", newForCommand);
		actions.Add("Scoped Repetition End", newEndForCommand);
		actions.Add("Scoped Repetition Comparison", newForComparisonCommand);

		// Adding If Command to dictionary
		actions.Add("Scoped Repetition If", newIfCommand);

		// Adding Comparisons to dictionary
		comparisons.Add("Comparing Asteroid Number", newComparison);

	}
	
	public GameObject handleEvent(string eventType) {
		GameObject box;
		if (eventType.Contains("Comparing")) {
			box = interpreter.addComparison(comparisons[eventType]());
		} else {
			box = interpreter.addCommand((Command) (actions [eventType])());
			if (eventType.Contains("Scoped Repetition")) {
				interpreter.addCommand((Command)(actions ["Scoped Repetition End"])());
			}
		}
		return box;
	}
}
