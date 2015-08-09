using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class DataKeeper : Enduring {
	private static Dictionary<string, object> persistentData = new Dictionary<string, object>();

	public static void save(string key, object data) {
		persistentData.Remove(key);
		persistentData.Add(key, data);
	}

	public static object retrieve(string key) {
		object dataRetrieved = persistentData.ContainsKey(key) ? persistentData[key] : null;
		persistentData.Remove(key);
		return dataRetrieved;
	}
}