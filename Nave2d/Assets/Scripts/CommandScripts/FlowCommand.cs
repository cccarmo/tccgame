using UnityEngine;
using UnityEngine.UI;

public delegate bool BoolCondition();
public delegate bool FlowCallback(BoolCondition cond, ref int programCounter);


public enum VariableForComparison{AsteroidsAhead, ForceFieldAhead, BatteryAhead, none};


public class FlowCommand : Command {
	private BoolCondition condition;
	protected FlowCallback flowCallback;

	public int intToCompare;
	public bool negateComparison = false;
	private VariableForComparison variableForComparison = VariableForComparison.none;
	private bool wasAlreadyUsed = false;
	private bool isLoop;

	public override void resetRepetitionCounter () {
		repetitionCounter = 0;
		wasAlreadyUsed = false;
	}

	// Constructor
	public FlowCommand(FlowCallback flowCallback, string label, GameObject boxPreFab, int indentLevel, bool IsLoop) {
		isLoop = IsLoop;
		this.label = label;
		this.commandBoxPreFab = boxPreFab;

		this.flowCallback = flowCallback;
		this.indentLevel = indentLevel;

		repetitionCounter = 0;
		repetitionMax = 1;
		condition = KFunctionRepeat;
	}

	public void setTypeOfComparison(VariableForComparison type) {
		variableForComparison = type;
		condition = KFunctionCompare;
	}
	
	// methods to compare int variables
	private int getIntValueToCompare () {
		switch (variableForComparison) {
		case VariableForComparison.AsteroidsAhead:
			return GameObject.FindGameObjectsWithTag("Asteroid").Length;
		}
		return -1;
	}

	private bool KFunctionCompare() {
		// Add this to make usable for If as well
		if (!isLoop) {
			if (wasAlreadyUsed) {
				wasAlreadyUsed = false;
				return false;
			} else {
				wasAlreadyUsed = true;
			}
		}
		
		bool answer = false;
		switch(variableForComparison) {
			case VariableForComparison.AsteroidsAhead:
				answer = GameObject.FindWithTag ("ObjectDetector").GetComponent<ObjectDetector> ().getCollisionType (1);
				break;
			case VariableForComparison.ForceFieldAhead:
				answer = GameObject.FindWithTag ("ObjectDetector").GetComponent<ObjectDetector> ().getCollisionType (2);
				break;
			case VariableForComparison.BatteryAhead:
				answer = GameObject.FindWithTag ("ObjectDetector").GetComponent<ObjectDetector> ().getCollisionType (3);
				break;
			default: return false;
		}
		if (negateComparison) {
			if (answer == true) {
				wasAlreadyUsed = false;
			}
			return !answer;
		}
		else {
			if (answer == false) {
				wasAlreadyUsed = false;
			}
			return answer;
		}
	}

	private bool KFunctionRepeat(){
		if (repetitionCounter < repetitionMax) {
			repetitionCounter++;
			return true;
		} else {
			repetitionCounter = 0;
			return false;
		}
	}
	
	public override bool execute(ref int programCounter) {
		return flowCallback(condition, ref programCounter);
	}
}
