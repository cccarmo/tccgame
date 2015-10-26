using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	// Deletar a inicialização quando a referencia vier de fora automaticamente pela setModuleAndLevel.
	public static int currentModule = 2;
	public static int currentLevel = 0;
	private static GameObject currentLevelGameObject;
	public static bool[] showPigAtLevel;

	void Start () {
		showPigAtLevel = new bool[30];
		for (int i = 0; i < 30; i++) {
			showPigAtLevel[i] = true;
		}

		// Adicionar aqui os niveis que não terão o porquinho falando, as dezenas são o módulo e as unidades o level
		// Ex: modulo: 1 level: 0 = showPigAtLevel[10]

		string moduleName = "Module" + currentModule;
		GameObject module = GameObject.Find(moduleName) as GameObject;
		if (module != null) {
			module.GetComponent<LevelHolder> ().levels [currentLevel].SetActive (true);
		}
	}

	public static void setModuleAndLevel (int m, int l) {
		currentModule = m;
		currentLevel = l;
	}

	public void goToNextLevel () {
		AudioPlayer.winMusic.Stop ();
		if (currentLevel == 9) {
			currentModule++;
			currentLevel = 0;
		}
		else {
			currentLevel++;
		}
	}
}
