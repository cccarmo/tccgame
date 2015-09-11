using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

	private int module;

	public void setModule (int m) {
		module = m;
	}

	public void selectLevel (int level) {
		LevelController.setModuleAndLevel (module, level);
	}
}
