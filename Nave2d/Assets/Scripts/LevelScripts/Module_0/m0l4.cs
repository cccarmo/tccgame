using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class m0l4 : MonoBehaviour {

	private Button buttonToSetOff;
	private GameObject fabricToSetOff;
	private GameObject tabToSetOff;

	// Use this for initialization
	void Start () {
		buttonToSetOff = GameObject.Find ("Controll").GetComponent<Button> ();
		buttonToSetOff.enabled = false;
		buttonToSetOff = GameObject.Find ("Variable").GetComponent<Button> ();
		buttonToSetOff.enabled = false;
		fabricToSetOff = GameObject.Find ("Shoot Button");
		fabricToSetOff.SetActive (false);
		fabricToSetOff = GameObject.Find ("Left Button");
		fabricToSetOff.SetActive (false);
		fabricToSetOff = GameObject.Find ("Right Button");
		fabricToSetOff.SetActive (false);
		
		tabToSetOff = GameObject.Find ("ControllTab");
		tabToSetOff.SetActive (false);
		
		tabToSetOff = GameObject.Find ("VariableTab");
		tabToSetOff.SetActive (false);

		GameObject panel = GameObject.FindWithTag ("DropPanel");
		CommandInterpreter interpreter = panel.GetComponent<CommandInterpreter> ();
		interpreter.SetMaxCommands (20);
	}

}
