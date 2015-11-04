using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class m2l8 : MonoBehaviour {

	private GameObject tabToSetOff;

	// Use this for initialization
	void Start() {
		GameObject panel = GameObject.FindWithTag("DropPanel");
		CommandInterpreter interpreter = panel.GetComponent<CommandInterpreter>();
		interpreter.SetMaxCommands(20);
		
		tabToSetOff = GameObject.Find("ControllTab");
		tabToSetOff.SetActive(false);
		tabToSetOff = GameObject.Find("VariableTab");
		tabToSetOff.SetActive(false);
	}
	
}
