using UnityEngine;
using System.Collections;

public class PigHead : MonoBehaviour {

	Animator leftEye;
	Animator rightEye;
	Animator head;

	// Use this for initialization
	void Start () {
		head = GetComponents<Animator> () [0];
		leftEye = GameObject.Find ("EyeLeft").GetComponent<Animator>();
		rightEye = GameObject.Find ("EyeRight").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
		float n = Random.Range (0f, 100f);
		if (n < .5f) {
			Blink();
		}

	}

	public void Talk() {
		head.Play("PigTalk");
		GetComponent<AudioSource> ().Play ();
		Blink ();
	}

	public void Blink() {
		rightEye.Play ("PigBlink");
		leftEye.Play ("PigBlink");
	}

}
