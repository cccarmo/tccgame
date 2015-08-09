using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CommandBox : MonoBehaviour {
	public CommandCreator commandCreator;
	private int index;
	public string label;
	private Vector3 originalPosition;
	private Vector3 mousePosition;
	public float moveSpeed = 1f;
	private bool dragging = false;
	private bool newEvent = false;
	private Vector2 touchOffset;
	private Color highlightColor;
	public Command command;
	private Vector3 offset;
	private Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
	private int ticks;

	public void Init(int index, Command command) {
		this.command = command;
		SetIndex(index);
		highlightColor = new Color(0.1f, 0.5f, 0.5f, 1);
	}

	public void SetIndex(int index) {
		this.index = index;
	}

	public void Highlight() {
		GetComponent<Image>().color = highlightColor;
	}


	void Start() {
		offset = Vector3.zero;
		ticks  = 0;
	}


	void Update() {
		if(Screen.width != screenRect.width || Screen.height != screenRect.height)
			screenRect = new Rect(0, 0, Screen.width, Screen.height);

		if(dragging) {
			mousePosition = Input.mousePosition;
			if(!screenRect.Contains(mousePosition)) {
				dragging = false;
				transform.position = originalPosition;
			}
			else {
				mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
				transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed) + touchOffset;
			}
		}
	
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
		originalPosition = transform.position;
		mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		touchOffset = originalPosition - mousePosition;

		transform.SetAsLastSibling();
		dragging = true;
	}

	void OnMouseUp() {
		dragging = false;
	}

	void OnMouseUpAsButton() {
		if (commandCreator != null) {
			commandCreator.handleEvent(label);
			transform.position = originalPosition;
		}
	}
}
