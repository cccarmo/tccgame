using UnityEngine;
using System.Collections;

public class AudioPlayer : Enduring {
	public static AudioSource bgMusic;
	public static AudioSource winMusic;
	
	void Start() {
		bgMusic = GetComponents<AudioSource>()[0];
		winMusic = GetComponents<AudioSource>()[1];
	}
}
