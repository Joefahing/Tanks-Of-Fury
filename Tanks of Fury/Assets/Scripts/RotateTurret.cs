using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurret : MonoBehaviour {
	void Update () {
		if (GlobalVariables.currentPlayerHasFiredCannon == false) {
			if (Input.GetKey ("q")) {
				//rotate camera left along y axis
				GlobalVariables.currentPlayerTurret.transform.RotateAround (GlobalVariables.currentPlayerTurret.transform.position, Vector3.down, GlobalVariables.moveSpeed * Time.deltaTime);//rotate around self
			}

			if (Input.GetKey ("e")) {
				//move camera right along y axis
				GlobalVariables.currentPlayerTurret.transform.RotateAround (GlobalVariables.currentPlayerTurret.transform.position, Vector3.up, GlobalVariables.moveSpeed * Time.deltaTime);//rotate around self
			}
		}
	}
}