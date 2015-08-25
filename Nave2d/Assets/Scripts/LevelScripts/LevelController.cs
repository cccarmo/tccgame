﻿using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	// Deletar a inicialização quando a referencia vier de fora automaticamente pela setModuleAndLevel.
	private static int currentModule = 99;
	private static int currentLevel = 0;
	private static GameObject currentLevelGameObject;

	void Start () {
		string moduleName = "Module" + currentModule;
		GameObject module = GameObject.Find(moduleName) as GameObject;
		module.GetComponent<LevelHolder> ().levels [currentLevel].SetActive (true);
	}

	public static void setModuleAndLevel (int m, int l) {
		currentModule = m;
		currentLevel = l;
	}
}