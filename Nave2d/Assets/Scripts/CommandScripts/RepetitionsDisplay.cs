using UnityEngine;
using System.Collections;
using UnityEngine.UI;

delegate void ButtonAction();

public class RepetitionsDisplay : MonoBehaviour {
	private Command command;
	private CommandBox commandBox;
	private Text repetitions;
	private bool clicked, holding;
	private int performedActions;
	private float elapsedTime;
	private ButtonAction updateNumberOfRepetitions;

	private static float clickTransitionTime = 0.5f;

	void Start() {
		commandBox = gameObject.GetComponentInParent<CommandBox>();
		command = commandBox.command;
		repetitions = GetComponent<Text>();
		repetitions.text = command.repetitionMax.ToString() + "x";
		clicked = holding = false;
	}
	
	private void increaseNumberOfRepetitions() {
		command.repetitionMax = Mathf.Min(command.repetitionMax + 1, 99);
		repetitions.text = command.repetitionMax.ToString() + "x";
	}

	private void decreaseNumberOfRepetitions() {
		command.repetitionMax = Mathf.Max(command.repetitionMax - 1, 1);
		repetitions.text = command.repetitionMax.ToString() + "x";
	}

	public void accumulateOnHolding() {
		updateNumberOfRepetitions = increaseNumberOfRepetitions;
		onClick();
	}

	public void decreaseOnHolding() {
		updateNumberOfRepetitions = decreaseNumberOfRepetitions;
		onClick();
	}

	public void onEnter() {
		if(!commandBox.dragging())
			commandBox.disableDrag();
	}

	public void onExit() {
		commandBox.enableDrag();
	}

	private void onClick() {
		holding = false;
		clicked = true;
		performedActions = 0;
		performButtonAction();
	}

	public void onRelease() {
		clicked = holding = false;
	}

	private void performButtonAction() {
		updateNumberOfRepetitions();
		performedActions++;
		elapsedTime = 0 * Time.deltaTime;
	}

	private void calculateElapsedTime() {
		elapsedTime += Time.deltaTime;
	}

	void Update() {
		calculateElapsedTime();
		if(clicked && elapsedTime > clickTransitionTime) {
			clicked = false;
			holding = true;
		}
		if (holding && elapsedTime > clickTransitionTime/performedActions)
			performButtonAction();
	}
}
