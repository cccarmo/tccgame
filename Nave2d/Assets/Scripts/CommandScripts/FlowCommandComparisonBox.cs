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
			comparisonBoxCollider = collider;
			holdingComparison = true;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.tag == "Comparison") {
			comparisonBoxCollider = null;
			holdingComparison = false;
		}
	}

	void OnMouseEnter() {
		if (holdingComparison) {
			attachCollider(comparisonBoxCollider);
		}
	}
	
	void OnMouseExit() {
		if (holdingComparison) {
			isComplete = false;
			comparisonBoxCollider.GetComponent<ComparisonBox>().disattach();
		}
	}

	private void attachCollider (Collider2D collider) {
		isComplete = true;
		FlowCommand flowCommand = (FlowCommand)command;
		comparisonBox = collider.GetComponent<ComparisonBox>();
		comparisonBox.attach(flowCommand);
		Vector3 newPosition = transform.position;
		newPosition.x += (GetComponent<BoxCollider2D>().bounds.size.x + collider.GetComponent<BoxCollider2D>().bounds.size.x) / 2;
		collider.transform.position = newPosition;
		collider.transform.SetParent(transform);
		collider.transform.localScale = transform.localScale;
	}


}
