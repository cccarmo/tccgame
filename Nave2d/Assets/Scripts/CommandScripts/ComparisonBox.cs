﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

public class ComparisonBox : MonoBehaviour {
	private Vector3 originalPosition;
	public float moveSpeed = 1f;
	private Vector2 touchOffset;
	private Color highlightColor;
	private Vector3 offset;
	private int ticks;
	private bool attached = false;
	public bool pressed;
	private ComparisonAttacher comparisonAttacher;
	private Comparison comparison;
	public VariableForComparison variableForComparison;


	public GameObject negativeImage;
	public GameObject positiveImage;
	
	public void Init(Comparison comparison) {
		this.comparison = comparison;
	}

	public void attach(FlowCommand flowCommand, ComparisonAttacher cAttacher) {
		attached = true;
		comparisonAttacher = cAttacher;
		comparison.configureFlowCommand(flowCommand, variableForComparison);
		highlightColor = new Color(0.1f, 0.5f, 0.5f, 1);
	}

	public void disattach () {
		GameObject Panel = GameObject.FindWithTag ("DropPanel");
		transform.SetParent(Panel.transform);
		comparisonAttacher.disattach();
		attached = false;
	}
	
	public void Highlight() {
		GetComponent<Image>().color = highlightColor;
	}
	
	void Start() {
		offset = Vector3.zero;
		ticks  = 0;
		if (comparison != null) {
			if (comparison.negateComparison) {
				if (positiveImage.activeSelf) {
					changePositiveNegative();
				} 
			} else {
				if (negativeImage.activeSelf) {
					changePositiveNegative();
				} 
			}
		}
	}
	
	void Update() {
		if (!attached) {
			if (ticks > 0) {
				ticks = ticks - 1;
				transform.position = transform.position + offset;
			}
		
			if (pressed) {
				Vector3 mousePosition = Input.mousePosition;
				mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);
				transform.position = Vector2.Lerp (transform.position, mousePosition, moveSpeed);
			}
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
		transform.SetAsLastSibling ();
		GetComponentInParent<Transform> ().SetAsLastSibling ();
	}

	public double Dist(Vector2 a, Vector2 b) {
		return Math.Sqrt ((a.x - b.x)*(a.x - b.x) + (a.y - b.y)*(a.y - b.y));
	}

	public void OnMouseUp() {
		if (!attached) {
			Destroy(this.transform.gameObject);
		}
		pressed = false;
	}

	public void OnMouseExit() {
		if (pressed) {
			if (attached) {
					disattach ();
			}
		}
	}
	
	public Comparison getComparison() {
		return comparison;
	}

	public void changePositiveNegative () {
		if (positiveImage.activeSelf) {
			positiveImage.SetActive(false);
			negativeImage.SetActive(true);
			comparison.shouldNegateComparison(true);
		} else {
			positiveImage.SetActive(true);
			negativeImage.SetActive(false);
			comparison.shouldNegateComparison(false);
		}
	}
	
}
