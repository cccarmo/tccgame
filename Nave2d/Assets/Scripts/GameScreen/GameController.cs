using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
	private GameObject gameScreen;
	public GameObject VictoryPopUp;
	private static GameObject victoryPopUp;

	void Start() {
		// TrashCan bizarre bug fix
		Vector3 position = GameObject.FindWithTag ("TrashCan").transform.position;
		position.x += 0.01f;

		GameObject.FindWithTag("TrashCan").transform.position = position;
		
		victoryPopUp = VictoryPopUp;

		if (AudioPlayer.bgMusic != null)
			AudioPlayer.bgMusic.Play();
	}

	public static void SetVictoryPopUpVisible () {
		victoryPopUp.SetActive(true);
	}
}

