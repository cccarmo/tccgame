using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

public class CommandBox : MonoBehaviour {
	private float moveSpeed = 1f;
	private Color highlightColor;
	private Color normalColor;
	public Command command;
	private Vector3 offset;
	private int ticks;
	private bool pressed;

	public void Init(Command command) {
		this.command   = command;
		highlightColor = new Color(0.1f, 0.5f, 0.5f, 1);
		normalColor    = new Color(1f, 1f, 1f, 1);
		normalColor    = new Color(0f, 0f, 0f, 1);
	}

	public void Highlight() {
		GetComponent<Image>().color = highlightColor;
	}

	public void NormalHighlight() {
		GetComponent<Image>().color = normalColor;
	}


	void Start() {
		offset = Vector3.zero;
		ticks  = 0;
	}

	void Update() {
		if(ticks > 0) {
			ticks = ticks - 1;
			transform.position = transform.position + offset;
		}

		if (pressed) {
			Vector3 mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
			transform.position = Vector2.Lerp (transform.position, mousePosition, moveSpeed);
		}
	}

	public void GoToPos(Vector3 position) {
		ticks = 8;
		
		Vector3 oldPos = transform.position;
		transform.localPosition = position;
		Vector3 newPos = transform.position;
		transform.position = oldPos;

		offset = (newPos - transform.position)/ticks;	
	}


	public void OnMouseDown() {
		pressed = true;
		transform.SetAsLastSibling();
	}

	public void OnMouseUp() {
		pressed = false;
	}
}
