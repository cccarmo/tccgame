using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;


public class FlowCommandComparisonBox : CommandBox {

	private ComparisonBox comparisonBox;
	bool isComplete = false;
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Comparison") {
			isComplete = true;
			attachCollider(collider);
		}
	}

	void OnMouseEnter() {
	}
	
	void OnMouseExit() {
	}


	private void attachCollider (Collider2D collider) {
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
