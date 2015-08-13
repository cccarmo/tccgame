using UnityEngine;
using UnityEngine.UI;

abstract public class Command {
	public int indentLevel;
	public string label;
	protected GameObject commandBoxPreFab;
	
	public override string ToString() {
		return label;
	}

	public GameObject getCommandBoxPreFab() {
		return commandBoxPreFab;
	}

	abstract public bool execute(ref int programCounter);
}
