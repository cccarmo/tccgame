using UnityEngine;
using System.Collections;

public class Comparison {
	public FlowCommand command;
	public VariableForComparison variableForComparison;
	public bool negateComparison;
	private GameObject comparisonBoxPreFab;

	public Comparison(GameObject boxPreFab) {
		negateComparison = false;
		comparisonBoxPreFab = boxPreFab;
	}

	public void configureFlowCommand(FlowCommand flowCommand, VariableForComparison v) {
		command = flowCommand;
		command.intToCompare = 0;
		command.negateComparison = negateComparison;
		variableForComparison = v;
		command.setTypeOfComparison(variableForComparison);
	}

	public GameObject getComparisonBoxPreFab() {
		return comparisonBoxPreFab;
	}

	public void shouldNegateComparison(bool b) {
		negateComparison = b;
		command.negateComparison = negateComparison;
	}
	
}
