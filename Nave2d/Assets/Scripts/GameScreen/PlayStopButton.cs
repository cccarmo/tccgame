using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayStopButton : MonoBehaviour {
	public Sprite playButton;
	public Sprite stopButton;
	private Image icon;
	public SimulationManager manager;

	void Start() {
		icon = gameObject.GetComponent<Image>();
	}

	void Update() {
		if(manager.simulationRunning())
			icon.sprite = stopButton;
		else 
			icon.sprite = playButton;
	}
}
