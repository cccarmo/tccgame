using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	// Deletar a inicialização quando a referencia vier de fora automaticamente pela setModuleAndLevel.
	public static int currentModule = 2;
	public static int currentLevel = 8;
	private static GameObject currentLevelGameObject;
	public static bool[] showPigAtLevel;

	void Start () {
		showPigAtLevel = new bool[30];
		for (int i = 0; i < 30; i++) {
			showPigAtLevel[i] = false;
		}
		
		// Adicionar aqui os niveis que não terão o porquinho falando, as dezenas são o módulo e as unidades o level
		// Ex: modulo: 1 level: 0 = showPigAtLevel[10]
		showPigAtLevel[00] = true;
		showPigAtLevel[01] = true;
		showPigAtLevel[03] = true;
		showPigAtLevel[04] = true;
		showPigAtLevel[06] = true;
		showPigAtLevel[08] = true;
		showPigAtLevel[09] = true;

		showPigAtLevel[10] = true;
		showPigAtLevel[14] = true;
		showPigAtLevel[17] = true;
		showPigAtLevel[19] = true;

		showPigAtLevel[20] = true;
		showPigAtLevel[22] = true;
		showPigAtLevel[25] = true;
		showPigAtLevel[27] = true;
		showPigAtLevel[29] = true;

		string moduleName = "Module" + currentModule;
		GameObject module = GameObject.Find(moduleName) as GameObject;
		if (module != null) {
			currentLevelGameObject = module.GetComponent<LevelHolder>().levels[currentLevel];
			currentLevelGameObject.SetActive(true);
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
