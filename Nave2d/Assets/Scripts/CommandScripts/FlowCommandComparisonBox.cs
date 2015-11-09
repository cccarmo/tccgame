using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;


public class FlowCommandComparisonBox : FlowCommandBox {

	public bool isComplete = false;
	private Collider2D comparisonBoxCollider;
	public ComparisonAttacher comparisonAttacher;


	public void attach() {
		isComplete = true;
	}

	public void disattach() {
		isComplete = false;
	}

	public override bool hasAttachments() {
		return isComplete;
	}

	public ComparisonBox getComparisonBox() {
		return comparisonAttacher.getComparisonBox();
	}
}
