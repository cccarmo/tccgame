using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;


public class FlowCommandBox : CommandBox {

	private CommandBox endOfScope;

	public void setEndOfScopeAsChild (CommandBox EndOfScope) {
		endOfScope = EndOfScope;
		endOfScope.transform.SetParent (transform);
		endOfScope.transform.localScale = new Vector2 (1, 1);
	}

	public void setEndUnderBox () {
		Vector3 pos = transform.position;
		pos.y -= (GetComponent<Collider2D>().bounds.size.y * 1.45f) * (GetComponentsInChildren<CommandBox>().Length - 1);
		endOfScope.transform.position = pos;
	}

	public override void onClick() {
		if(isEnabled) {
			setAllChildrenInactive();
			pressed = true;
			transform.SetAsLastSibling();
		}
	}

}
