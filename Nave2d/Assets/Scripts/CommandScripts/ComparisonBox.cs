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
	private int ticks;
	private bool attached = false;
	public bool pressed;
	private FlowCommandComparisonBox commandBox;
	private bool disattacheable;

	private Vector2 originalTouchPosition;

	public InputField inputFieldValue;

	public VariableForComparison variableForComparison;

	public void attach(FlowCommand flowCommand, FlowCommandComparisonBox cBox) {
		attached = true;
		commandBox = cBox;
		command = flowCommand;
		command.comparrison = TypeOfComparison.equals;
		command.intToCompare = 0;
		command.negateComparrison = false;
		command.setTypeOfComparison(variableForComparison);
		highlightColor = new Color(0.1f, 0.5f, 0.5f, 1);

	}

	public void disattach () {
		commandBox.disattach ();
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
		disattacheable = true;
		pressed = true;
		transform.SetAsLastSibling ();
		originalTouchPosition = Input.mousePosition;
		GetComponentInParent<Transform> ().SetAsLastSibling ();
	}

	public double Dist(Vector2 a, Vector2 b) {
		return Math.Sqrt ((a.x - b.x)*(a.x - b.x) + (a.y - b.y)*(a.y - b.y));
	}

	public void OnMouseDrag() {
		if (attached) {
			if (Dist(originalTouchPosition, Input.mousePosition) > 10 && disattacheable) {
				disattach ();
				disattacheable = false;
			}
		}
	}

	public void OnMouseUp() {
		if (!attached) {
			Destroy(this.transform.gameObject);
		}
		originalTouchPosition = Vector2.zero;
		pressed = false;
	}

	
	public void changeNegativePositive(bool b) {
		command.negateComparrison = b;
	}

	public void changeComparisonType(int type) {
		switch (type) {
		case 0: command.comparrison = TypeOfComparison.equals;
			break;
		case 1: command.comparrison = TypeOfComparison.lesser;
			break;
		case 2: command.comparrison = TypeOfComparison.greater;
			break;
		case 3: command.comparrison = TypeOfComparison.lesserOrEquals;
			break;
		case 4: command.comparrison = TypeOfComparison.greaterOrEquals;
			break;
		}
	}

	public void changeIntToCompare() {
		if (inputFieldValue.text.IsNullOrWhiteSpace()) {
			command.intToCompare = 0;
		} else {
			command.intToCompare = Convert.ToInt32 (inputFieldValue.text);
		}
	}
}
