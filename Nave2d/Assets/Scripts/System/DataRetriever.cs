using UnityEngine;
using System.Collections;

public abstract class DataRetriever : MonoBehaviour {
	private string key;
	
	void Awake() {
		key = this.name;
	}
	
	protected object retrieveData() {
		return DataKeeper.retrieve(key);
	}
	
	protected void saveData(object data) {
		DataKeeper.save(key, data);
	}
}
