using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;


public class FlowCommandComparisonBox : CommandBox {

	private ComparisonBox comparisonBox;
	
	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log("Colidiu");
		if (collider.tag == "Comparison") {
			FlowCommand flowCommand = (FlowCommand)command;
			comparisonBox = collider.GetComponent<ComparisonBox>();
			comparisonBox.Init(flowCommand);
			collider.transform.position = transform.position;
			collider.transform.SetParent(transform);
		}
	}

}
