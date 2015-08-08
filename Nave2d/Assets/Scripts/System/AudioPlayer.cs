using UnityEngine;
using System.Collections;

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
