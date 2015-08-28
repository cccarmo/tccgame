using UnityEngine;
using UnityEngine.UI;

public delegate bool BoolCondition();
public delegate bool FlowCallback(BoolCondition cond, ref int programCounter);


public enum TypeOfComparisson {equals, greater, lesser, greaterOrEquals, lesserOrEquals, none};
public enum VariableForComparisson {numberOfAsteroids, none};


public class FlowCommand : Command {
	private BoolCondition condition;
	protected FlowCallback flowCallback;

	public int repetitionMax;
	public int repetitionCounter;
	public int intToCompare;
	public TypeOfComparisson comparrison = TypeOfComparisson.none;
	public bool negateComparrison = false;
	private VariableForComparisson variableForComparisson = VariableForComparisson.none;
	private bool wasAlreadyUsed = false;
	private bool isLoop;

	// Constructor
	public FlowCommand (FlowCallback flowCallback, string label, GameObject boxPreFab, int indentLevel, bool IsLoop) {
		isLoop = IsLoop;
		this.label = label;
		this.commandBoxPreFab = boxPreFab;

		this.flowCallback = flowCallback;
		this.indentLevel = indentLevel;
		
		repetitionMax = 0;
		repetitionCounter = 0;
		condition = KFunctionRepeate;
	}

	public void setTypeOfComparisson (VariableForComparisson type) {
		variableForComparisson = type;
		switch (type) {
		case VariableForComparisson.numberOfAsteroids: condition = KFunctionCompareIntValues;
			break;
		}
	}
	
	// methods to compare int variables
	private int getIntValueToCompare () {
		switch (variableForComparisson) {
		case VariableForComparisson.numberOfAsteroids:
			return GameObject.FindGameObjectsWithTag("Asteroid").Length;
		}
		return -1;
	}

	private bool KFunctionCompareIntValues () {

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
		switch (comparrison) {
		case TypeOfComparisson.equals:
			if (variableValue == intToCompare) {
				answer = true;
			}
			break;
		case TypeOfComparisson.greater:
			if (variableValue > intToCompare) {
				answer = true;
			}
			break;
		case TypeOfComparisson.lesser:
			if (variableValue < intToCompare) {
				answer = true;
			}	
			break;
		case TypeOfComparisson.greaterOrEquals:
			if (variableValue >= intToCompare) {
				answer = true;
			}
			break;
		case TypeOfComparisson.lesserOrEquals:
			if (variableValue <= intToCompare) {
				answer = true;
			}
			break;
		default: return false;
		}
		
		if (negateComparrison) {
			return !answer;
		}
		else {
			return answer;
		}
	}
	
	public bool KFunctionRepeate (){
		if (repetitionCounter < repetitionMax) {
			repetitionCounter++;
			return true;
		} else {
			repetitionCounter = 0;
			return false;
		}
	}
	
	public override bool execute(ref int programCounter) {
		return flowCallback (condition, ref programCounter);
	}
}
