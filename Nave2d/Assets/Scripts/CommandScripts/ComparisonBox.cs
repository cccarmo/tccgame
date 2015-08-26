using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

public class ComparisonBox : MonoBehaviour {
	private Vector3 originalPosition;
	public float moveSpeed = 1f;
	private Vector2 touchOffset;
	private Color highlightColor;
	public FlowCommand command;
	private Vector3 offset;
	private Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
	private int ticks;
	private bool attached = false;
	private bool pressed;

	public InputField inputFieldValue;

	public VariableForComparisson variableForComparisson;

	public void attach(FlowCommand command) {
		attached = true;
		this.command = command;
		command.comparrison = TypeOfComparisson.equals;
		command.intToCompare = 0;
		command.negateComparrison = false;
		command.setTypeOfComparisson (variableForComparisson);
		highlightColor = new Color(0.1f, 0.5f, 0.5f, 1);
	}

	public void disattach () {
		attached = false;
		command = null;
	}
	
	public void Highlight() {
		GetComponent<Image>().color = highlightColor;
	}
	
	void Start() {
		offset = Vector3.zero;
		ticks  = 0;
	}
	
	void Update() {
		if (!attached) {
			if (ticks > 0) {
				ticks = ticks - 1;
				transform.position = transform.position + offset;
			}
		
			if (pressed) {
				Vector3 mousePosition = Input.mousePosition;
				mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
				transform.position = Vector2.Lerp (transform.position, mousePosition, moveSpeed);
			}
		}
	}
	
	public void GoToPos(Vector3 position) {
		ticks = 8;
		
		Vector3 oldPos = transform.position;
		transform.localPosition = position;
		Vector3 newPos = transform.position;
		transform.position = oldPos;
		
		offset = (newPos - transform.position)/ticks;	
	}
	
	
	public void OnMouseDown() {
		pressed = true;
		transform.SetAsLastSibling();
	}
	
	public void OnMouseUp() {
		if (!attached) {
			Destroy(this.transform.gameObject);
		}
		pressed = false;
	}

	
	public void changeNegativePositive (bool b) {
		command.negateComparrison = b;
	}

	public void changeComparissonType (int type) {
		switch (type) {
		case 0: command.comparrison = TypeOfComparisson.equals;
			break;
		case 1: command.comparrison = TypeOfComparisson.lesser;
			break;
		case 2: command.comparrison = TypeOfComparisson.greater;
			break;
		case 3: command.comparrison = TypeOfComparisson.lesserOrEquals;
			break;
		case 4: command.comparrison = TypeOfComparisson.greaterOrEquals;
			break;
		}
	}

	public void changeIntToCompare () {
		if (inputFieldValue.text.IsNullOrWhiteSpace()) {
			command.intToCompare = 0;
		} else {
			command.intToCompare = Convert.ToInt32 (inputFieldValue.text);
		}
	}
}
