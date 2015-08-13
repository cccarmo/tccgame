using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;


public class FlowCommandBox : CommandBox {
	public InputField numberOfRepetitions;

	
	public void updateNumberOfRepetitions () {
		FlowCommand flowCommand = (FlowCommand)command;
		if (numberOfRepetitions.text.IsNullOrWhiteSpace()) {
			flowCommand.repetitionMax = 0;
		}
		flowCommand.repetitionMax = Math.Abs (Convert.ToInt32 (numberOfRepetitions.text));
	}
}
