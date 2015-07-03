using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody2D body;
	public Boundary boundary;
	public float tilt;
	public GameObject Shot;
	public Transform ShotSpawn;
	public float fireRate;
	private float nextFire = 0;

	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		body.velocity = movement * speed;

		body.position = new Vector2 (Mathf.Clamp(body.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp(body.position.y, boundary.yMin, boundary.yMax));

		body.rotation = body.velocity.x * (-tilt);
	}


	void Update() {

		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			GetComponent<AudioSource>().Play();
			nextFire = Time.time + fireRate;
			Instantiate (Shot, ShotSpawn.position, ShotSpawn.rotation);

		}

	}

}
