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
	public int index;
	public Command command;
	private CommandInterpreter commandInterpreter;


	public void Init(string commandLabel, int index, Command command) {
		commandText = gameObject.GetComponentInChildren<Text>();
		commandText.text = commandLabel;

		this.index = index;
		this.command = command;

		highlightColor = new Color (0.1f, 0.5f, 0.5f, 1);
	}


	public void Highlight() {
		GetComponent<Image> ().color = highlightColor;
	}


	void Start() {
		commandInterpreter = this.GetComponentInParent<CommandInterpreter> ();
	}

	void Update() {
		if(dragging) {
			mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
			transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed) + touchOffset;
		}
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
			commandCreator.handleEvent (label);
			transform.position = originalPosition;
		}

		if (commandInterpreter != null) {
			commandInterpreter.FixOrderOfBlock (this);
		}
	}
}
