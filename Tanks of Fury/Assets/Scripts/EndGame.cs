using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		endGame ();
	}

	public void endGame(){
		if (GlobalVariables.playerOneCurrentHealth == 0 || GlobalVariables.playerTwoCurrentHealth == 0) {
			Debug.Log ("Game Over");
		}
	}
}
