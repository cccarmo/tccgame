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
