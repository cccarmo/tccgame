using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;


public class FlowCommandComparisonBox : CommandBox {

	private ComparisonBox comparisonBox;
	private bool isComplete = false;
	private bool holdingComparison;
	private Collider2D comparisonBoxCollider;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Comparison") {
			if (!isComplete) {
				comparisonBoxCollider = collider;
				comparisonBox = collider.GetComponent<ComparisonBox>();
			}
		}
	}
	
	void OnMouseEnter() {
		if (comparisonBox != null && comparisonBox.pressed) {
			attachCollider(comparisonBoxCollider);
		}
	}
	
	void OnMouseExit() {
		if (comparisonBox.pressed) {
			disattach();
			comparisonBoxCollider.GetComponent<ComparisonBox>().disattach();
		}
	}

	private void attachCollider (Collider2D collider) {
		isComplete = true;
		FlowCommand flowCommand = (FlowCommand)command;
		comparisonBox.attach(flowCommand, this);
		Vector3 newPosition = transform.position;
		newPosition.x += 0.2f + (GetComponent<BoxCollider2D>().bounds.size.x + collider.GetComponent<BoxCollider2D>().bounds.size.x) / 2;
		collider.transform.position = newPosition;
		collider.transform.SetParent(transform);
		collider.transform.localScale = transform.localScale;
	}

	public void disattach() {
		isComplete = false;
	}
}
