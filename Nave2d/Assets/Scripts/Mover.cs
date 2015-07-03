using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	private Rigidbody2D body;
	public float speed;


	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody2D> ();
		float seno = Mathf.Sin (Mathf.Deg2Rad *(body.rotation - 90f));
		float coseno = Mathf.Cos (Mathf.Deg2Rad * (body.rotation - 90f));

		body.velocity = new Vector2 (-seno , coseno)  * speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
