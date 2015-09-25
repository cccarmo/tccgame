using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class m0l5: MonoBehaviour {

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
		
		tabToSetOff = GameObject.Find ("ControllTab");
		tabToSetOff.SetActive (false);
		
		tabToSetOff = GameObject.Find ("VariableTab");
		tabToSetOff.SetActive (false);
	}

}
