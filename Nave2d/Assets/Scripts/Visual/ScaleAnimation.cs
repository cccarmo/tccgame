using UnityEngine;
using System.Collections;

public class ScaleAnimation : MonoBehaviour {
	private Vector3 originalLocalScale;
	private readonly float maxVariationProportion = 0.20f;
	private float currentProportion = 0;
	private int signal = 1;

	private void scale(float variation) {
		float factor = variation + 1.0f - maxVariationProportion;
		transform.localScale = new Vector3(factor * originalLocalScale.x, factor * originalLocalScale.y, originalLocalScale.z);
	}

	// Use this for initialization
	void Start () {
		originalLocalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		currentProportion += signal * maxVariationProportion/25;
		if(currentProportion <= 0 || currentProportion >= maxVariationProportion)
			signal = -signal;
		scale(currentProportion);
	}
}
