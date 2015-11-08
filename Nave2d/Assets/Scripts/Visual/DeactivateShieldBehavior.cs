﻿using UnityEngine;
using System.Collections;

public class DeactivateShieldBehavior : StateMachineBehaviour {

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		GameObject Shield = GameObject.FindWithTag("Shield");
		SpriteRenderer sprite = Shield.GetComponent<SpriteRenderer> ();
		
		Color opaque = new Color(1f,1f,1f,1f);
		sprite.color = opaque;
	}


	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		GameObject Shield = GameObject.FindWithTag("Shield");
		SpriteRenderer sprite = Shield.GetComponent<SpriteRenderer> ();
		
		Color transparent = new Color(1f,1f,1f,0f);
		sprite.color = transparent;
	}
}
