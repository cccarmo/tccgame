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
		int hittingType = GameObject.FindWithTag ("ObjectDetector").GetComponent<ObjectDetector> ().getCollisionType ();
		switch(variableForComparison) {
			case VariableForComparison.AsteroidsAhead:
				if (hittingType == 1) {
					answer = true;
				}
				break;
			case VariableForComparison.ForceFieldAhead:
				if (hittingType == 2) {
					answer = true;
				}
				break;
			case VariableForComparison.BatteryAhead:
				if (hittingType == 3) {
					answer = true;
				}
				break;
			default: return false;
		}
		
		if (negateComparison) {
			return !answer;
		}
		else {
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
