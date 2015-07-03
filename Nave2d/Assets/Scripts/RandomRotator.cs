using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

	public float tumble;
	private Rigidbody2D body;


	void Start () {
		body = GetComponent<Rigidbody2D> ();
		body.angularVelocity = Random.value * tumble;
	}
}
