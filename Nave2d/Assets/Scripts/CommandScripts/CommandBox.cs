using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

public class CommandBox : MonoBehaviour {
	public PanelListener eventListener;
	public string label;
	private Vector3 originalPosition;
	public float moveSpeed = 1f;
	private Vector2 touchOffset;
	private Color highlightColor;
	private Color normalColor;
	public Command command;
	private Vector3 offset;
	private Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
	private int ticks;

	public void Init(Command command) {
		this.command   = command;
		highlightColor = new Color(0.1f, 0.5f, 0.5f, 1);
		normalColor    = new Color(1f, 1f, 1f, 1);
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
		originalPosition = transform.position;
	}

	void Update() {
		if(Screen.width != screenRect.width || Screen.height != screenRect.height)
			screenRect = new Rect(0, 0, Screen.width, Screen.height);
		
		if(ticks > 0) {
			ticks = ticks - 1;
			transform.position = transform.position + offset;
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


	void OnMouseDown() {
		Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		touchOffset = originalPosition - mousePosition;

		transform.SetAsLastSibling();
	}

	void OnMouseDrag() {
		Vector3 mousePosition = Input.mousePosition;
		if(!screenRect.Contains(mousePosition))
			transform.position = originalPosition;
		else {
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
			transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed) + touchOffset;
		}
	}

	void OnMouseUp() {
		if (eventListener != null) {
			transform.position = originalPosition;
			eventListener.handleEvent(label);
		}
	}
}
