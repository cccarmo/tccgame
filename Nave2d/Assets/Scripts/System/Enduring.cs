using UnityEngine;
using System.Collections;

public abstract class Enduring : MonoBehaviour {
	private static ArrayList lastingObjects = new ArrayList();
	private static ArrayList instantiated = new ArrayList();

	void Awake() {
		if (instantiated.Contains(name) && !lastingObjects.Contains(gameObject))
			Destroy(this.gameObject);
		else {
			DontDestroyOnLoad(transform.gameObject);
			lastingObjects.Add(gameObject);
			if(!instantiated.Contains(name))
				instantiated.Add(name);
		}
	}
	
	public void restartInReload() {
		instantiated.Remove(name);
		lastingObjects.Remove(gameObject);
		Destroy(gameObject);
	}

	public static Enduring[] getLastingObjects() {
		Enduring[] list = new Enduring[lastingObjects.Count];
		int nextPosition = 0;
		foreach(object obj in lastingObjects) {
			list[nextPosition] = ((GameObject) obj).GetComponent<Enduring>();
			nextPosition++;
		}
		return list;
	}
}
