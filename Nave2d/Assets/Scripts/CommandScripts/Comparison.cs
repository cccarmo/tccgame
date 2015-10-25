using UnityEngine;
using System.Collections;

public class Comparison {
	public FlowCommand command;
	public VariableForComparison variableForComparison;
	private GameObject comparisonBoxPreFab;

	public Comparison(GameObject boxPreFab) {
		comparisonBoxPreFab = boxPreFab;	
	}

	public void setFlowCommand(FlowCommand flowCommand) {
		command = flowCommand;
		command.comparison = TypeOfComparison.equals;
		command.intToCompare = 0;
		command.negateComparison = false;
		command.setTypeOfComparison(variableForComparison);
	}

	public GameObject getComparisonBoxPreFab() {
		return comparisonBoxPreFab;
	}

	public void shouldNegateComparison(bool b) {
		command.negateComparison = b;
	}

	public void changeType(int type) {
		switch(type) {
		case 0: command.comparison = TypeOfComparison.equals;
			break;
		case 1: command.comparison = TypeOfComparison.doesNotEqual;
			break;
		case 2: command.comparison = TypeOfComparison.lesser;
			break;
		case 3: command.comparison = TypeOfComparison.lesserOrEquals;
			break;
		case 4: command.comparison = TypeOfComparison.greater;
			break;
		case 5: command.comparison = TypeOfComparison.greaterOrEquals;
			break;
		}
	}
}
