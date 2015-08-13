using UnityEngine;
using UnityEngine.UI;

public delegate bool CommandCallback();

public class ShipCommand : Command {
	protected CommandCallback callback;

	public ShipCommand(CommandCallback callback, string label, GameObject boxPreFab) {
		this.label = label;
		this.commandBoxPreFab = boxPreFab;
		this.callback = callback;

		this.indentLevel = 0;
	}
	
	public override bool execute(ref int programCounter) {
		return callback ();
	}
}
