using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayerTwo : MonoBehaviour {
	public GameObject healthBarParent;

	private GameObject[] healthBars;//local reference to all the health bars
	private int childCount;//number of health bars, allows for change without script editing
	private int randomMod;

	void Start() {
		int childCount = healthBarParent.transform.childCount;

		healthBars = new GameObject[childCount];

		GlobalVariables.playerTwoCurrentHealth = childCount;
		GlobalVariables.playerTwoMaxHealth = childCount;

		//get the list of health bars that the player has and store them locally
		for (int i = 0; i < childCount; i++){
			//Debug.Log ("Health Bar: " + healthBarParent.transform.GetChild(i));
			healthBars [i] = healthBarParent.transform.GetChild (i).gameObject;
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.tag == "AOEDamage") {
			//Debug.Log ("Damage Taken");
			GlobalVariables.playerTwoCurrentHealth--;//subtract a health unit from player 2
			healthBars [GlobalVariables.playerTwoCurrentHealth].GetComponent<Renderer> ().material = Resources.Load ("Invisible", typeof(Material)) as Material;

			if (GlobalVariables.playerTwoCurrentHealth <= 0) {
				Debug.Log ("Player 2: Died!");
			}
		}

		if (collider.tag == "HealthPack") {
			Destroy (collider.gameObject);

			Debug.Log ("Player 2: Recovered Health");
			if (GlobalVariables.playerTwoCurrentHealth != GlobalVariables.playerTwoMaxHealth) {
				healthBars [GlobalVariables.playerTwoCurrentHealth].GetComponent<Renderer> ().material = Resources.Load ("Green", typeof(Material)) as Material;
				GlobalVariables.playerTwoCurrentHealth++;//add health unit to player 2
			}
		}

		if (collider.tag == "FuelPack") {
			Destroy (collider.gameObject);

			Debug.Log ("Player 2: Recovered Fuel");
			GlobalVariables.currentPlayerFuelLevel = GlobalVariables.playerTwoMaxTravelDistance;
			GlobalVariables.currentPlayerFuelText.text = GlobalVariables.currentPlayerFuelLevel.ToString();
		}

		if (collider.tag == "AmmoMod") {
			Destroy (collider.gameObject);
			//Randomly assign the player a "mod"
			randomMod = Random.Range(0,2);//[min] is inclusive [max] is exclusive\

			if (randomMod == 0) {
				Debug.Log ("Player 2: BIG EXPLOSION MOD ACTIVATED");
				GlobalVariables.playerTwoSizeAOE *= 2;
				GlobalVariables.sizeAOE = GlobalVariables.playerTwoSizeAOE;//reload variable

			} 
			else if (randomMod == 1) {
				Debug.Log ("Player 2: ADVANCED FUEL ACTIVATED");
				GlobalVariables.playerTwoMaxTravelDistance += 500;
				GlobalVariables.maxTravelDistance = GlobalVariables.playerTwoMaxTravelDistance;//reload variable
				GlobalVariables.currentPlayerFuelLevel = GlobalVariables.playerTwoMaxTravelDistance;//refuel player
				GlobalVariables.currentPlayerFuelText.text = GlobalVariables.currentPlayerFuelLevel.ToString();
			}
		}
	}
}