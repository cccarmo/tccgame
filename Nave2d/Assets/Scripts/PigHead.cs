using UnityEngine;
using System.Collections;

public class PigHead : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Talk() {
		GetComponents<Animator>()[0].Play("PigTalk");
		GetComponent<AudioSource> ().Play ();
		Animator pigEye = GameObject.Find ("EyeRight").GetComponent<Animator>();
		pigEye.Play ("PigBlink");
		pigEye = GameObject.Find ("EyeLeft").GetComponent<Animator>();
		pigEye.Play ("PigBlink");
	}
}
