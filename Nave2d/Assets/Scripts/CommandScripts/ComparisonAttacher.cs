using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;


public class ComparisonAttacher : MonoBehaviour {

	private ComparisonBox comparisonBox;
	private Collider2D comparisonBoxCollider;
	public FlowCommandComparisonBox FCCBox;
	
	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log (collider.tag);
		if (collider.tag == "Comparison") {
			if (!FCCBox.isComplete) {
				comparisonBoxCollider = collider;
				comparisonBox = collider.GetComponent<ComparisonBox>();
			}
		}
		if (collider.tag == "MouseDetector") {
			Debug.Log("AQUI2.8");
			if (comparisonBox != null) {
				Debug.Log("AQUI3");
				if (comparisonBox.pressed) {
					attachCollider();
				}
			}
		}
	}

	public void setComparison(GameObject comparisonObject) {
		comparisonBoxCollider = comparisonObject.GetComponent<Collider2D>();
		comparisonBox = comparisonObject.GetComponent<ComparisonBox>();
	}

	public void attachCollider() {
		FCCBox.attach ();
		comparisonBox.attach((FlowCommand)FCCBox.command, this);
		Vector3 newPosition = transform.position;
		newPosition.x += (GetComponent<Collider2D>().bounds.size.x + comparisonBoxCollider.bounds.size.x)  *  0.38f;
		comparisonBoxCollider.transform.position = newPosition;
		comparisonBoxCollider.transform.SetParent(transform);
		comparisonBoxCollider.transform.localScale = transform.localScale;
	}

	public void disattach() {
		FCCBox.disattach ();
	}
	

	public ComparisonBox getComparisonBox() {
		return comparisonBox;
	}
}
