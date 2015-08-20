using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;


public class SemanticInterpreter {
	private CommandInterpreter commandInterpreter;

	public SemanticInterpreter(CommandInterpreter commandInterpreter) {
		this.commandInterpreter = commandInterpreter;
	}


	public int getEndForFromIndex(int index) {
		ArrayList commandList = commandInterpreter.makeCommandListFromCommandsDrawn();

		int nestLevel = 0;
		for (int i = index; i < commandList.Count; i++) {
			Command c = (Command) commandList[i];
			if (c.label == "Scoped Repetition End") {
				if (nestLevel == 0)
					return i;
				else
					nestLevel--;
			}

			if (c.label == "Scoped Repetition")
				nestLevel++;
		}

		return 0;
	}


	public int getBeginForFromIndex(int index) {
		ArrayList commandList = commandInterpreter.makeCommandListFromCommandsDrawn();
		
		int nestLevel = 0;
		for (int i = index; i >= 0; i--) {
			Command c = (Command) commandList[i];
			if (c.label == "Scoped Repetition") {
				if (nestLevel == 0)
					return i;
				else
					nestLevel--;
			}
			
			if (c.label == "Scoped Repetition End")
				nestLevel++;
		}
		
		return 0;
	}

	
	public bool ForCommand(BoolCondition condition, ref int programCounter){
		if (condition() == false)
			programCounter = getEndForFromIndex(programCounter) + 1;
		return true;
	}

	public bool EndForCommand(BoolCondition condition, ref int programCounter){
		// Subtract 2 to jump the endFor
		programCounter = getBeginForFromIndex(programCounter - 2);
		return true;
	}
}
