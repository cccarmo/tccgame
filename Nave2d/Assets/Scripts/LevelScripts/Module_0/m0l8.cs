using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class m0l8 : MonoBehaviour {

	private Button buttonToSetOff;
	private GameObject tabToSetOff;

	// Use this for initialization
	void Start () {
		buttonToSetOff = GameObject.Find ("Controll").GetComponent<Button> ();
		buttonToSetOff.enabled = false;
		buttonToSetOff = GameObject.Find ("Variable").GetComponent<Button> ();
		buttonToSetOff.enabled = false;
		
		tabToSetOff = GameObject.Find ("ControllTab");
		tabToSetOff.SetActive (false);
		
		tabToSetOff = GameObject.Find ("VariableTab");
		tabToSetOff.SetActive (false);

		GameObject panel = GameObject.FindWithTag ("DropPanel");
		CommandInterpreter interpreter = panel.GetComponent<CommandInterpreter> ();
		interpreter.SetMaxCommands (20);
	}

}
