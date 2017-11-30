using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoAutomate : MonoBehaviour {

	private bool isGrounded;
	private int jumpSpeed = 120000;

	private bool routineStarted = false;
	public Camera aiCamera;

	private Quaternion currentDir;
	private Vector3 targetDir;
	Vector3 newDir;
	Quaternion rotation;
	
	// Update is called once per frame
	void FixedUpdate () {
		if (GlobalVariables.CurrentPlayerTurn == 2) {
			if (!GlobalVariables.currentPlayerHasFiredCannon && !GlobalVariables.currentPlayerMaxTravelDistance) {
				Vector3 cameraOffset = aiCamera.transform.position - GlobalVariables.currentPlayerTank.transform.position;

				Vector3 playerOnePosition = GlobalVariables.playerOneTank.transform.position;

				if (gameObject.transform.forward.x != playerOnePosition.x
					&& gameObject.transform.forward.z != playerOnePosition.z) {
					//make AI tank look at the player

					if (!routineStarted) {
						StartCoroutine (FireCannon());
						routineStarted = true;
					}

					currentDir = gameObject.transform.rotation;
					targetDir = playerOnePosition - gameObject.transform.position;
					newDir = Vector3.RotateTowards (gameObject.transform.forward, targetDir, Time.deltaTime, 0.0f);
					rotation = Quaternion.LookRotation (newDir);

					gameObject.transform.rotation = Quaternion.Slerp(currentDir, rotation, Time.deltaTime * GlobalVariables.moveSpeed);

				} 



			}

		}
	}

	IEnumerator FireCannon(){
		yield return new WaitForSecondsRealtime (5f);
		GlobalVariables.fireAI = true;
		routineStarted = false;
	}

}
