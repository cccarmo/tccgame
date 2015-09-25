using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class m0l0: MonoBehaviour {

	private Button buttonToSetOff;
	private GameObject fabricToSetOff;

	// Use this for initialization
	void Start () {
		buttonToSetOff = GameObject.Find ("Controll").GetComponent<Button> ();
		buttonToSetOff.enabled = false;
		buttonToSetOff = GameObject.Find ("Variable").GetComponent<Button> ();
		buttonToSetOff.enabled = false;
		fabricToSetOff = GameObject.Find ("Down Button");
		fabricToSetOff.SetActive (false);
		fabricToSetOff = GameObject.Find ("Shoot Button");
		fabricToSetOff.SetActive (false);
		fabricToSetOff = GameObject.Find ("Left Button");
		fabricToSetOff.SetActive (false);
		fabricToSetOff = GameObject.Find ("Right Button");
		fabricToSetOff.SetActive (false);
		fabricToSetOff = GameObject.Find ("Clockwise Button");
		fabricToSetOff.SetActive (false);
		fabricToSetOff = GameObject.Find ("Counterclockwise Button");
		fabricToSetOff.SetActive (false);

		GameObject scroll = GameObject.FindWithTag ("ScrollPanel");
		ScrollRect scrollRect = scroll.GetComponent<ScrollRect> ();
		scrollRect.enabled = false;
		GameObject scrollBar = GameObject.FindWithTag ("ScrollBar");
		scrollBar.SetActive(false);
		GameObject panel = GameObject.FindWithTag ("DropPanel");
		CommandInterpreter interpreter = panel.GetComponent<CommandInterpreter> ();
		interpreter.SetMaxCommands (9);

	}

}
