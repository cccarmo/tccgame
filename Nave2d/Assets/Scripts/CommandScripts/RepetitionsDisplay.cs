using UnityEngine;
using System.Collections;
using UnityEngine.UI;

delegate void ButtonAction();

public class RepetitionsDisplay : MonoBehaviour {
	private Command command;
	private Text repetitions;
	private bool clicked, holding;
	private int performedActions;
	private float startTime, elapsedTime;
	private ButtonAction updateNumberOfRepetitions;

	private static float clickTransitionTime = 0.5f;

	void Start() {
		command = gameObject.GetComponentInParent<CommandBox>().command;
		repetitions = GetComponent<Text>();
		clicked = holding = false;
	}
	
	private void increaseNumberOfRepetitions() {
		command.repetitionMax = Mathf.Min(command.repetitionMax + 1, 9);
		repetitions.text = command.repetitionMax.ToString() + "x";
	}

	private void decreaseNumberOfRepetitions() {
		command.repetitionMax = Mathf.Max(command.repetitionMax - 1, 1);
		repetitions.text = command.repetitionMax.ToString() + "x";
	}

	public void accumulateOnHolding() {
		Debug.Log("clicou em +");
		updateNumberOfRepetitions = increaseNumberOfRepetitions;
		onClick();
	}

	public void decreaseOnHolding() {
		updateNumberOfRepetitions = decreaseNumberOfRepetitions;
		onClick();
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
