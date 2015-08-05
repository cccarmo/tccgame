using UnityEngine;
using System.Collections;

public class ScreenTransition : MonoBehaviour {

	private GameObject panelFade;

	void Start () {
		panelFade = GameObject.Find("PanelFade");
	}

	//Transition
	public void LoadNextLevel(string name) {
		StartCoroutine(LevelLoad(name));
		panelFade.GetComponent<Animator>().Play("FadeOut");
	}
	
	IEnumerator LevelLoad(string name) {
		yield return new WaitForSeconds(0.333f);
		Application.LoadLevel(name);
	}


}
