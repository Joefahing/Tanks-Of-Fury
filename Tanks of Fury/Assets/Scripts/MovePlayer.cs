using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//camera will now follow player, camera controls are currently disabled
public class MovePlayer : MonoBehaviour {
	private bool isGrounded;
	private int jumpSpeed = 120000;

	public Camera playerCamera;
	Vector3 cameraOffset;

	void Update () {
		if (GlobalVariables.currentPlayerHasFiredCannon == false) {
			if (GlobalVariables.currentPlayerMaxTravelDistance == false) {
				cameraOffset = playerCamera.transform.position - GlobalVariables.currentPlayerTank.transform.position;
				//moving the player based on user input
				if (Input.GetKey ("w")) {
					//move object in the forward direction thats its facing
					GlobalVariables.currentPlayerTank.transform.position += GlobalVariables.currentPlayerTank.transform.forward * GlobalVariables.moveSpeed * Time.deltaTime;
					updateFuelConsumption ();
				}

				if (Input.GetKey ("s")) {
					//move object in the backward direction thats its facing
					GlobalVariables.currentPlayerTank.transform.position -= GlobalVariables.currentPlayerTank.transform.forward * GlobalVariables.moveSpeed * Time.deltaTime;
					updateFuelConsumption ();
				}

				if (Input.GetKey("space")) {
					//need to check to see if the sphere is on the ground, to prevent 'flying'
					if (isGrounded == true) {
						//GlobalVariables.currentPlayerTank.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpSpeed * Time.deltaTime);
						isGrounded = false;
					}
				} 
			} 
			else {
				GlobalVariables.currentPlayerFuelLevel = 0;
				GlobalVariables.currentPlayerFuelText.text = GlobalVariables.currentPlayerFuelLevel.ToString();
			}
	
			if (Input.GetKey ("a")) {
				//rotate camera left along y axis
				GlobalVariables.currentPlayerTank.transform.RotateAround (GlobalVariables.currentPlayerTank.transform.position, Vector3.up, 2 * (-1) * GlobalVariables.moveSpeed * Time.deltaTime);//rotate around self
			
				playerCamera.transform.RotateAround (gameObject.transform.position, Vector3.up, (-1) * GlobalVariables.moveSpeed * Time.deltaTime);
				cameraOffset = playerCamera.transform.position - GlobalVariables.currentPlayerTank.transform.position;
			}

			if (Input.GetKey ("d")) {
				//move camera right along y axis
				GlobalVariables.currentPlayerTank.transform.RotateAround (GlobalVariables.currentPlayerTank.transform.position, Vector3.up, 2 * GlobalVariables.moveSpeed * Time.deltaTime);//rotate around self
			
				playerCamera.transform.RotateAround (gameObject.transform.position, Vector3.up, GlobalVariables.moveSpeed * Time.deltaTime);
				cameraOffset = playerCamera.transform.position - GlobalVariables.currentPlayerTank.transform.position;
			}
		}
	}
		
	void OnCollisionEnter(Collision collision) {
		isGrounded = true;//the player has returned to the 'ground'
		GlobalVariables.currentPlayerTank.GetComponent<Rigidbody>().velocity = Vector3.zero;
		GlobalVariables.currentPlayerTank.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}

	void OnCollisionExit(Collision collision) {	
		isGrounded = false;//the player has gone airborne, don't let them jump
	}

	void updateFuelConsumption() {
		GlobalVariables.currentPlayerFuelLevel -= (int) (Vector3.Distance(GlobalVariables.currentPlayerStartingLocation,GlobalVariables.currentPlayerTank.transform.position)) / GlobalVariables.fuelMultiplier;
		GlobalVariables.currentPlayerFuelText.text = GlobalVariables.currentPlayerFuelLevel.ToString();

		if (GlobalVariables.currentPlayerFuelLevel <= 0) {
			GlobalVariables.currentPlayerMaxTravelDistance = true;
		}
	}
}