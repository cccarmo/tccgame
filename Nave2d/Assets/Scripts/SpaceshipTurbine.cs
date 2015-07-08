using UnityEngine;
using System.Collections;

public class SpaceshipTurbine : MonoBehaviour {
	private GameObject spaceship;

	// Use this for initialization
	void Start() {
		spaceship = transform.parent.gameObject;
		particleSortingLayerFix();
	}
	
	// Update is called once per frame
	void Update() {
	
	}

	private void particleSortingLayerFix() {
		ParticleSystem turbine = GetComponent<ParticleSystem>();
		string spaceshipLayer = spaceship.GetComponent<Renderer>().sortingLayerName;
		turbine.GetComponent<Renderer>().sortingLayerName = spaceshipLayer;
		turbine.GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = -1;
	}
}
