using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public static class GameObjectExtentions {
	public static GameObject[] FindChildrenWithTag(this GameObject go, string tag) {
		return go.transform.Cast<Transform>().Where(c => c.gameObject.tag == tag).Select(t => t.gameObject).ToArray();
	}
}

public class SpeechBalloon : MonoBehaviour {

	private GameObject[] balloonTexts;
	private int index;
	public Button buttonNext;
	public PigHead pigHead;

	// Use this for initialization
	void Start () {
		balloonTexts = gameObject.FindChildrenWithTag("Balloon");
		index = 0;
		if (balloonTexts.Length == 1) {
			buttonNext.gameObject.SetActive(false);
		}
		for (int i = 1; i < balloonTexts.Length; i++) {
			balloonTexts[i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clickNext () {
		pigHead.Talk ();
		balloonTexts[index].SetActive(false);
		index++;
		balloonTexts[index].SetActive(true);
		if (index > balloonTexts.Length - 2) {
			buttonNext.gameObject.SetActive(false);
		} 
	}
}
