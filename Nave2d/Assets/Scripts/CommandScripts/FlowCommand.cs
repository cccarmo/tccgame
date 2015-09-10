using UnityEngine;
using UnityEngine.UI;

public delegate bool BoolCondition();
public delegate bool FlowCallback(BoolCondition cond, ref int programCounter);


public enum TypeOfComparison{equals, doesNotEqual, lesser, lesserOrEquals, greater, greaterOrEquals, none};
public enum VariableForComparison{numberOfAsteroids, none};


public class FlowCommand : Command {
	private BoolCondition condition;
	protected FlowCallback flowCallback;

	public int intToCompare;
	public TypeOfComparison comparison = TypeOfComparison.none;
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
		switch (type) {
		case VariableForComparison.numberOfAsteroids: condition = KFunctionCompareIntValues;
			break;
		}
	}
	
	// methods to compare int variables
	private int getIntValueToCompare () {
		switch (variableForComparison) {
		case VariableForComparison.numberOfAsteroids:
			return GameObject.FindGameObjectsWithTag("Asteroid").Length;
		}
		return -1;
	}

	private bool KFunctionCompareIntValues() {
		// Add this to make usable for If as well
		if (!isLoop) {
			if (wasAlreadyUsed) {
				wasAlreadyUsed = false;
				return false;
			} else {
				wasAlreadyUsed = true;
			}
		}
		
		int variableValue = getIntValueToCompare ();
		bool answer = false;
		switch(comparison) {
			case TypeOfComparison.equals:
				if (variableValue == intToCompare) {
					answer = true;
				}
				break;
			case TypeOfComparison.doesNotEqual:
				if (variableValue != intToCompare) {
					answer = true;
				}
				break;
			case TypeOfComparison.lesser:
				if (variableValue < intToCompare) {
					answer = true;
				}
				break;
			case TypeOfComparison.lesserOrEquals:
				if (variableValue <= intToCompare) {
					answer = true;
				}
				break;
			case TypeOfComparison.greater:
				if (variableValue > intToCompare) {
					answer = true;
				}
				break;
			case TypeOfComparison.greaterOrEquals:
				if (variableValue >= intToCompare) {
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
