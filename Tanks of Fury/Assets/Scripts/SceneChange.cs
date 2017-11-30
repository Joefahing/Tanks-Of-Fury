using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {


	public void ChangeToScene(string sceneToChangeTo){
		SceneManager.LoadScene(sceneToChangeTo);
		Time.timeScale = 1;
		if (sceneToChangeTo == "PVP") {
			GlobalVariables.mode = "PVP";
		} else if (sceneToChangeTo == "PVE") {
			GlobalVariables.mode = "PVM";
		}
	}
}
