using UnityEngine;
using UnityEngine.UI;

public delegate bool CommandCallback();
public delegate bool FlowCallback(ref int programCounter);


public class Command {
	private CommandCallback callback;
	private FlowCallback flowCallback;
	private string label;
	private GameObject commandBoxPreFab;
	private bool isFlowCommand;
	
	public Command(CommandCallback callback, FlowCallback flowCallback, string label, GameObject boxPreFab, bool isFlowCommand) {
		
		this.label = label;
		this.commandBoxPreFab = boxPreFab;
		this.isFlowCommand = isFlowCommand;
		
		this.callback = callback;
		this.flowCallback = flowCallback;
	}
	
	public override string ToString() {
		return label;
	}
	
	public bool execute(ref int programCounter) {
		if (this.isFlowCommand)
			return flowCallback(ref programCounter);
		
		else
			return callback ();
		
	}
	
	public GameObject getCommandBoxPreFab() {
		return commandBoxPreFab;
	}
}