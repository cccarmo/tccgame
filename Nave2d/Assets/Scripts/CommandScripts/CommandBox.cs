﻿using UnityEngine;
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
	public bool pressed = false;
	protected bool isEnabled = true;
	public bool isActive = true;

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
	}

	void FixedUpdate() {
		if (pressed) {
			Vector3 mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
			transform.position = Vector2.Lerp (transform.position, mousePosition, moveSpeed);
		}
	}

	public void GoToPos(Vector3 position) {
		Transform oldestParent;
		Vector3 newPos;

		for (oldestParent = transform; oldestParent.parent.tag != "DropPanel"; oldestParent = oldestParent.transform.parent);

		Vector3 oldPos = oldestParent.position;
		oldestParent.localPosition = position;
		newPos = oldestParent.position;
		oldestParent.position = oldPos;
		offset = (newPos - transform.position);

		transform.position = newPos;

		if (isActive && offset != Vector3.zero && !pressed)
			setAllChildrenActive ();

		offset = Vector3.zero;
	}


	public virtual void onClick() {
		if(isEnabled) {
			pressed = true;
			transform.SetAsLastSibling();
		}
	}

	public void onRelease() {
		pressed = false;
	}

	public bool dragging() {
		return pressed;
	}

	public void enableDrag() {
		isEnabled = true;
	}

	public void disableDrag() {
		isEnabled = false;
	}

	public virtual bool hasAttachments() {
		return false;
	}

	public void setAllChildrenInactive () {
		CommandBox[] children = GetComponentsInChildren<CommandBox>();
		foreach (CommandBox child in children) {
			if (child.transform.parent != transform.parent)
				child.isActive = false;
		}
	}
	
	public void setAllChildrenActive () {
		CommandBox[] children = GetComponentsInChildren<CommandBox>();
		
		foreach (CommandBox child in children) {
			if (child.transform.parent != transform.parent)
				child.isActive = true;
		}
	}
}
