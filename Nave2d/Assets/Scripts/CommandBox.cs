using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CommandBox : MonoBehaviour {
	public string label;
	public CommandCreator commandCreator;
	private Text commandText;
	private Vector3 originalPosition;
	private Vector3 mousePosition;
	public float moveSpeed = 1f;
	private bool dragging = false;
	private Vector2 touchOffset;
	private Color highlightColor;
	public Command command;
	private CommandInterpreter commandInterpreter;
	private Vector3 offset;
	private Rect screenRect = new Rect(0, 0, Screen.width, Screen.height);
	int ticks;


	public void SetLabelByIndex(int index) {
		commandText.text = index + " " + command.ToString();
	}

	public void Init(int index, Command command) {
		commandText = gameObject.GetComponentInChildren<Text>();
		this.command = command;

		SetLabelByIndex(index);
		highlightColor = new Color (0.1f, 0.5f, 0.5f, 1);
	}


	public void Highlight() {
		GetComponent<Image> ().color = highlightColor;
	}


	void Start() {
		offset = Vector3.zero;
		ticks  = 0;
		commandInterpreter = this.GetComponentInParent<CommandInterpreter>();
	}

	void Update() {
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

		/// Find Final Position
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

		dragging = true;
	}

	void OnMouseUpAsButton() {
		dragging = false;
		if (commandCreator != null) {
			commandCreator.handleEvent(label);
			transform.position = originalPosition;
		}
		if (commandInterpreter != null) {
			commandInterpreter.FixOrderOfBlock();
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (commandCreator == null && collider.tag == "TrashCan") {
			Destroy(transform.gameObject);
		}
	}
}
