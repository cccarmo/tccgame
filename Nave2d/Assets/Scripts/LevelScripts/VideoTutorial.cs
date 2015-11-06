using UnityEngine;
using System.Collections;

public class VideoTutorial : MonoBehaviour {

	void Start () {
		MovieTexture movie = ((MovieTexture)GetComponent<Renderer> ().material.mainTexture);
		movie.loop = true;
		movie.Play();
	}
}
