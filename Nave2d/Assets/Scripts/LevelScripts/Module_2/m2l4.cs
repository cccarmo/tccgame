﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class m2l4 : MonoBehaviour {

	private GameObject fabricToSetOff;
	private GameObject tabToSetOff;

	// Use this for initialization
	void Start() {
		fabricToSetOff = GameObject.Find("Shield Button");
		fabricToSetOff.SetActive(false);
		fabricToSetOff = GameObject.Find("DoWhile Button");
		fabricToSetOff.SetActive(false);
		
		GameObject scroll = GameObject.FindWithTag("ScrollPanel");
		ScrollRect scrollRect = scroll.GetComponent<ScrollRect>();
		scrollRect.enabled = false;
		GameObject scrollBar = GameObject.FindWithTag("ScrollBar");
		scrollBar.SetActive(false);
		GameObject panel = GameObject.FindWithTag("DropPanel");
		CommandInterpreter interpreter = panel.GetComponent<CommandInterpreter>();
		interpreter.SetMaxCommands(9);
		
		tabToSetOff = GameObject.Find("ControllTab");
		tabToSetOff.SetActive(false);
		tabToSetOff = GameObject.Find("VariableTab");
		tabToSetOff.SetActive(false);
	}

}
