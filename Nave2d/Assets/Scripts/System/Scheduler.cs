using UnityEngine;
using System.Threading;
using System.Collections;

public delegate void Action();

public abstract class Scheduler : MonoBehaviour {
	private IEnumerator waitFor(float seconds, Action delayedAction, params object[] arguments) {
		yield return new WaitForSeconds(seconds);
		delayedAction.DynamicInvoke(arguments);
	}

	protected void executeAfter(float seconds, Action delayedAction) {
		StartCoroutine(waitFor(seconds, delayedAction));
	}
}
