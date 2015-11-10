using UnityEngine;
using System.Collections;

public class VideoTutorial : MonoBehaviour {

	WebGLMovieTexture tex;
	public string moviePath;
	private bool isPLaying;

	void Start () {
		tex = new WebGLMovieTexture(moviePath);
		gameObject.GetComponent<Renderer>().material.mainTexture = tex;
		
		tex.loop = true;
		isPLaying = false;
	}

	void Update() {
		tex.Update();

		if ((!isPLaying) && tex.isReady) {
			isPLaying = true;
			tex.Play ();
		}
	}

	public void exitSceneCallback() {
		tex.Pause ();
	}
}
