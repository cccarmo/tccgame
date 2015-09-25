using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MoveOnBorder : MonoBehaviour {
	public int moveOffset;
	private bool shouldMove;
	public ScrollRect scrollRect;
	private readonly float speed = 0.005f;
	
	void Start () {
		shouldMove = false;
	}
	

	void Update () {
		if (scrollRect.enabled) {
			if (shouldMove) {
				scrollRect.verticalNormalizedPosition += moveOffset * speed;
			}

			if (Input.GetMouseButtonUp (0))
				shouldMove = false;
		}
	}

	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.GetComponent<CommandBox> () != null) {
			CommandBox c = collider.GetComponent<CommandBox> ();
			if (c.pressed) 
				shouldMove = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D collider) {
		if (collider.GetComponent<CommandBox> () != null) {
			CommandBox c = collider.GetComponent<CommandBox> ();
			if (c.pressed)
				shouldMove = false;

		}
	}
}
