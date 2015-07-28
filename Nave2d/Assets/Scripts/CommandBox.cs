using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class CommandBox : MonoBehaviour {
	private Text commandText;
	
	public void Init (string commandLabel) {
		commandText = gameObject.GetComponent<Text>();
		print (commandText);
		commandText.text = commandLabel;
	}
	
	void Update () {
		
	}
	
	void OnMouseOver () {
		print ("touch!");
		if(Input.GetMouseButtonDown(0)) {
			transform.position = Input.mousePosition;
		}
	}
}
