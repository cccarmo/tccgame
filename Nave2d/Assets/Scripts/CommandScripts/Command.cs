using UnityEngine;
using UnityEngine.UI;

public delegate bool CommandCallback();
public delegate bool BoolCondition();
public delegate bool FlowCallback(BoolCondition cond, ref int programCounter);




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
	
	public Command(CommandCallback callback, FlowCallback flowCallback, string label, GameObject boxPreFab, bool isFlowCommand, int indentLevel) {
		this.label = label;
		this.commandBoxPreFab = boxPreFab;
		this.isFlowCommand = isFlowCommand;
		
		this.callback = callback;
		this.flowCallback = flowCallback;
		this.indentLevel = indentLevel;

		repetitionMax = 3;
		repetitionCounter = 0;
		condition = KFunctionRepeate;
	}
	
	public override string ToString() {
		return label;
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
	
	public bool execute(ref int programCounter) {
		if (this.isFlowCommand)
			return flowCallback(condition, ref programCounter);
		
		else
			return callback ();		
	}
	
	public GameObject getCommandBoxPreFab() {
		return commandBoxPreFab;
	}
}