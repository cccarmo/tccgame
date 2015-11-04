using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class m2l3 : MonoBehaviour {

	private GameObject fabricToSetOff;
	private GameObject tabToSetOff;

	// Use this for initialization
	void Start() {
		fabricToSetOff = GameObject.Find("Shield Button");
		fabricToSetOff.SetActive(false);
		fabricToSetOff = GameObject.Find("DoWhile Button");
		fabricToSetOff.SetActive(false);

		GameObject panel = GameObject.FindWithTag("DropPanel");
		CommandInterpreter interpreter = panel.GetComponent<CommandInterpreter>();
		interpreter.SetMaxCommands(20);
		
		tabToSetOff = GameObject.Find("ControllTab");
		tabToSetOff.SetActive(false);
		tabToSetOff = GameObject.Find("VariableTab");
		tabToSetOff.SetActive(false);
	}

}
