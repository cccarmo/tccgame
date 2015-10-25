using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;


public class FlowCommandComparisonBox : FlowCommandBox {

	private ComparisonBox comparisonBox;
	public bool isComplete = false;
	private Collider2D comparisonBoxCollider;
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Comparison") {
			if (!isComplete) {
				comparisonBoxCollider = collider;
				comparisonBox = collider.GetComponent<ComparisonBox>();
			}
		} else if (collider.tag == "MouseDetector") {
			if (comparisonBox != null) {
				if (comparisonBox.pressed) {
					attachCollider();
				}
			}
		}
	}
	
	public void MouseExit() {
		if (comparisonBox != null) {
			if (comparisonBox.pressed) {
				comparisonBox.disattach();
			}
		}

	}

	private void attachCollider () {
		isComplete = true;
		FlowCommand flowCommand = (FlowCommand)command;
		comparisonBox.attach(flowCommand, this);
		Vector3 newPosition = transform.position;
		newPosition.x += 0.2f + (GetComponent<Collider2D>().bounds.size.x + comparisonBoxCollider.bounds.size.x) / 2;
		comparisonBoxCollider.transform.position = newPosition;
		comparisonBoxCollider.transform.SetParent(transform);
		comparisonBoxCollider.transform.localScale = transform.localScale;
	}

	public void disattach() {
		comparisonBoxCollider.transform.SetParent(transform.parent);
		isComplete = false;
	}

}
