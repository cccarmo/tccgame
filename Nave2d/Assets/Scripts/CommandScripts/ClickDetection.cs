using UnityEngine;
using System.Collections;

public class ClickDetection : MonoBehaviour {
	public bool isBeingClicked;
	private Collider2D collider;

	void Start () {
	}

	void OnMouseDown () {
		isBeingClicked = true;
	}

	void OnMouseUp () {
		isBeingClicked = false;
	}

	void OnMouseExit () {
		isBeingClicked = false;
	}
	
}
