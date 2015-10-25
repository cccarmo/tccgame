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
	private Vector3 offset;
	private int ticks;
	private bool attached = false;
	public bool pressed;
	private bool disattacheable;
	private FlowCommandComparisonBox commandBox;
	private Comparison comparison;

	private Vector2 originalTouchPosition;

	public InputField inputFieldValue;

	public void Init(Comparison comparison) {
		this.comparison = comparison;
	}

	public void attach(FlowCommand flowCommand, FlowCommandComparisonBox cBox) {
		attached = true;
		commandBox = cBox;
		comparison.setFlowCommand(flowCommand);
		highlightColor = new Color(0.1f, 0.5f, 0.5f, 1);

	}

	public void disattach () {
		commandBox.disattach();
		attached = false;
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
		comparison.shouldNegateComparison(b);
	}

	public void changeComparisonType(int type) {
		comparison.changeType(type);
	}

	public void changeIntToCompare() {
		if (inputFieldValue.text.IsNullOrWhiteSpace()) {
			comparison.command.intToCompare = 0;
		} else {
			comparison.command.intToCompare = Convert.ToInt32(inputFieldValue.text);
		}
	}
}
