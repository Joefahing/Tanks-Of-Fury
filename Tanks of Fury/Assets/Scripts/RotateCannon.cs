using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCannon : MonoBehaviour {
	void Update () {
		if (GlobalVariables.currentPlayerHasFiredCannon == false) {
			if (Input.GetKey ("z")) {
				if (GlobalVariables.currentPlayerCannon.transform.localEulerAngles.y > 140) {
					//rotate the cannon up
					GlobalVariables.currentPlayerCannon.transform.Rotate (Vector3.left * GlobalVariables.moveSpeed * Time.deltaTime);
				} 
			}

			if (Input.GetKey ("c")) {
				if (GlobalVariables.currentPlayerCannon.transform.localEulerAngles.y < 180) {
					//rotate the cannon down
					GlobalVariables.currentPlayerCannon.transform.Rotate (Vector3.right * GlobalVariables.moveSpeed * Time.deltaTime);
				}
			}
		}
	}
}