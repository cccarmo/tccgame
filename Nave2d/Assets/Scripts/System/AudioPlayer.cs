using UnityEngine;
using System.Collections;

public abstract class Enduring : MonoBehaviour {
	private bool created = false;

	void Awake() {
		if (created)
			Destroy(this.gameObject);
		else {
			DontDestroyOnLoad(transform.gameObject);
			created = true;
		}
	}

	public void restartInReload() {
		Destroy(this.gameObject);
	}
}

public class AudioPlayer : Enduring {
	public static AudioSource bgMusic;
	public static AudioSource winMusic;
	
	void Start() {
		if(bgMusic == null) {
			bgMusic = GetComponents<AudioSource>()[0];
			bgMusic.Play();
		}
		winMusic = GetComponents<AudioSource>()[1];
	}
}
