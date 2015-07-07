using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
	private float tumble;
	private Rigidbody2D body;
	
	void Start() {
		tumble = 500f;
		body = GetComponent<Rigidbody2D> ();
		body.angularVelocity = Random.value * tumble;
	}
}
