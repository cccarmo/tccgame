using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechBalloon : MonoBehaviour {

	private Text[] balloonTexts;
	private int index;
	public Button buttonNext;
	public PigHead pigHead;

	// Use this for initialization
	void Start () {
		balloonTexts = GetComponentsInChildren<Text> ();
		index = 0;
		if (balloonTexts.Length == 2) {
			buttonNext.gameObject.SetActive(false);
		}
		for (int i = 1; i < balloonTexts.Length - 1; i++) {
			balloonTexts[i].gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clickNext () {
		pigHead.Talk ();
		balloonTexts[index].gameObject.SetActive(false);
		index++;
		balloonTexts [index].gameObject.SetActive(true);
		if (index > balloonTexts.Length - 3) {
			buttonNext.gameObject.SetActive(false);
		} 
	}
}
