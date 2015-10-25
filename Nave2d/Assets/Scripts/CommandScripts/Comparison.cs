using UnityEngine;
using System.Collections;

public class Comparison {
	public FlowCommand command;
	public VariableForComparison variableForComparison;
	private bool negateComparison;
	private GameObject comparisonBoxPreFab;

	public Comparison(GameObject boxPreFab) {
		negateComparison = false;
		comparisonBoxPreFab = boxPreFab;
	}

	public void configureFlowCommand(FlowCommand flowCommand) {
		command = flowCommand;
		command.comparisonType = TypeOfComparison.equals;
		command.intToCompare = 0;
		command.negateComparison = negateComparison;
		command.setTypeOfComparison(variableForComparison);
	}

	public GameObject getComparisonBoxPreFab() {
		return comparisonBoxPreFab;
	}

	public void shouldNegateComparison(bool b) {
		negateComparison = b;
		command.negateComparison = negateComparison;
	}

	public void changeType(int type) {
		switch(type) {
		case 0: command.comparisonType = TypeOfComparison.equals;
			break;
		case 1: command.comparisonType = TypeOfComparison.doesNotEqual;
			break;
		case 2: command.comparisonType = TypeOfComparison.lesser;
			break;
		case 3: command.comparisonType = TypeOfComparison.lesserOrEquals;
			break;
		case 4: command.comparisonType = TypeOfComparison.greater;
			break;
		case 5: command.comparisonType = TypeOfComparison.greaterOrEquals;
			break;
		}
	}
}
