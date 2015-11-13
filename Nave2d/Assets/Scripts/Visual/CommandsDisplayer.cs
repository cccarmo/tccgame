using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CommandsDisplayer : MonoBehaviour {
	private CommandInterpreter interpreter;
	private Text displayer;
	private int addedCommands, maxCommands;
	private Color standardColor;
	
	void Start() {
		displayer = GetComponentInChildren<Text>();
		standardColor = displayer.color;

		GameObject panel = GameObject.FindWithTag("DropPanel");
		interpreter = panel.GetComponent<CommandInterpreter>();
	}

	void Update() {
		addedCommands = interpreter.getCommandsListCount();
		maxCommands = interpreter.getMaxCommands();
		displayer.text = addedCommands + "/" + maxCommands;

		if(addedCommands == maxCommands)
			displayer.color = Color.red;
		else displayer.color = standardColor;
	}
}
