using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MoveOnBorder : MonoBehaviour {
	public int moveOffset;
	private bool shouldMove;
	public ScrollRect scrollRect;
	private float elapsedTime = 0.0f;
	private readonly float speed = 0.007f;
	private readonly static float clickTransitionTime = 0.3f;
	
	void Start () {
		shouldMove = false;
	}
	

	void FixedUpdate () {
		calculateElapsedTime();
		if (scrollRect.enabled) {
			if (shouldMove && elapsedTime > clickTransitionTime) {
				scrollRect.verticalNormalizedPosition += moveOffset * speed;

				if (scrollRect.verticalNormalizedPosition < 0) {
					scrollRect.verticalNormalizedPosition = 0;
				}

				if (scrollRect.verticalNormalizedPosition > 1) {
					scrollRect.verticalNormalizedPosition = 1;
				}
			}

			if (Input.GetMouseButtonUp (0))
				shouldMove = false;
		}
	}

	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.GetComponent<CommandBox>() != null) {
			CommandBox c = collider.GetComponent<CommandBox>();
			if (c.pressed) {
				shouldMove = true;
				elapsedTime = 0.0f;
			}
		} else if (collider.GetComponent<ComparisonBox> () != null) {
			ComparisonBox c = collider.GetComponent<ComparisonBox>();
			if (c.pressed) {
				shouldMove = true;
				elapsedTime = 0.0f;
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D collider) {
		if (collider.GetComponent<CommandBox>() != null) {
			CommandBox c = collider.GetComponent<CommandBox>();
			if (c.pressed)
				shouldMove = false;

		} else if (collider.GetComponent<ComparisonBox>() != null) {
			ComparisonBox c = collider.GetComponent<ComparisonBox>();
			if (c.pressed) 
				shouldMove = false;
		}
	}

	private void calculateElapsedTime() {
		elapsedTime += Time.deltaTime;
	}
}
