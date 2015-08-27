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

	// Constructor
	public FlowCommand (FlowCallback flowCallback, string label, GameObject boxPreFab, int indentLevel) {
		this.label = label;
		this.commandBoxPreFab = boxPreFab;

		this.flowCallback = flowCallback;
		this.indentLevel = indentLevel;
		
		repetitionMax = 0;
		repetitionCounter = 0;
		condition = KFunctionRepeate;
	}

	public void setTypeOfComparisson (VariableForComparisson type) {
		Debug.Log("Setou o tipo de comparação");
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
			Debug.Log("Asteroides = " + GameObject.FindGameObjectsWithTag("Asteroid").Length);
			return GameObject.FindGameObjectsWithTag("Asteroid").Length;
		}
		return -1;
	}

	private bool KFunctionCompareIntValues () {
		int variableValue = getIntValueToCompare ();
		Debug.Log ("Ta fazendo e a comparacao é " + comparrison);
		bool answer = false;
		switch (comparrison) {
		case TypeOfComparisson.equals:
			if (variableValue == intToCompare) {
				Debug.Log("TRUE");
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
	
	public override bool execute(ref int programCounter) {
		return flowCallback (condition, ref programCounter);
	}
}
