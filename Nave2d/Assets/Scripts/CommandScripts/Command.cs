using UnityEngine;
using UnityEngine.UI;

public delegate bool CommandCallback();
public delegate bool BoolCondition();
public delegate bool FlowCallback(BoolCondition cond, ref int programCounter);


public enum TypeOfComparisson {equals, greater, lesser, greaterOrEquals, lesserOrEquals, none};
public enum VariableForComparisson {numberOfAsteroids, none};

public class Command {
	private CommandCallback callback;
	private FlowCallback flowCallback;
	public string label;
	private GameObject commandBoxPreFab;
	public bool isFlowCommand;
	private BoolCondition condition;
	public int indentLevel;

	public int repetitionMax;
	public int repetitionCounter;
	public int intToCompare;
	public TypeOfComparisson comparrison = TypeOfComparisson.none;
	public bool negateComparrison = false;
	private VariableForComparisson variableForComparisson = VariableForComparisson.none;

	
	public Command(CommandCallback callback, FlowCallback flowCallback, string label, GameObject boxPreFab, bool isFlowCommand, int indentLevel) {
		this.label = label;
		this.commandBoxPreFab = boxPreFab;
		this.isFlowCommand = isFlowCommand;
		
		this.callback = callback;
		this.flowCallback = flowCallback;
		this.indentLevel = indentLevel;

		repetitionMax = 0;
		repetitionCounter = 0;
		condition = KFunctionRepeate;
	}
	
	public override string ToString() {
		return label;
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

	public bool KFunctionCompare (){
		if (repetitionCounter < repetitionMax) {
			repetitionCounter++;
			return true;
		} else {
			repetitionCounter = 0;
			return false;
		}
	}
	
	public bool execute(ref int programCounter) {
		if (this.isFlowCommand) {
			return flowCallback (condition, ref programCounter);
		} else
			return callback();
	}

	public GameObject getCommandBoxPreFab() {
		return commandBoxPreFab;
	}
}
