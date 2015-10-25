using UnityEngine;
using UnityEngine.UI;

abstract public class Command {
	public int indentLevel;
	public string label;
	protected int repetitionCounter;
	public int repetitionMax;
	protected GameObject commandBoxPreFab;

	public override string ToString() {
		return label;
	}

	public GameObject getCommandBoxPreFab() {
		return commandBoxPreFab;
	}

	public void resetRepetitionCounter() {
		repetitionCounter = 0;
	}

	abstract public bool execute(ref int programCounter);
}
