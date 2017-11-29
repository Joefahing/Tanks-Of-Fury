using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayerOne : MonoBehaviour {
	public GameObject healthBarParent;

	private GameObject[] healthBars;//local reference to all the health bars
	private int childCount;//number of health bars, allows for change without script editing
	private int randomMod;

	void Start() {
		int childCount = healthBarParent.transform.childCount;

		healthBars = new GameObject[childCount];

		GlobalVariables.playerOneCurrentHealth = childCount;
		GlobalVariables.playerOneMaxHealth = childCount;

		//get the list of health bars that the player has and store them locally
		for (int i = 0; i < childCount; i++){
			//Debug.Log ("Health Bar: " + healthBarParent.transform.GetChild(i));
			healthBars [i] = healthBarParent.transform.GetChild (i).gameObject;
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.tag == "AOEDamage") {
			Debug.Log ("Player 1: Damage Taken");
			GlobalVariables.playerOneCurrentHealth--;//subtract a health unit from player 1
			healthBars [GlobalVariables.playerOneCurrentHealth].GetComponent<Renderer> ().material = Resources.Load ("Invisible", typeof(Material)) as Material;

			if (GlobalVariables.playerOneCurrentHealth <= 0) {
				Debug.Log ("Player 1: Died!");
			} 
		}

		if (collider.tag == "HealthPack") {
			Destroy (collider.gameObject);

			Debug.Log ("Player 1: Recovered Health");
			if (GlobalVariables.playerOneCurrentHealth != GlobalVariables.playerOneMaxHealth) {
				healthBars [GlobalVariables.playerOneCurrentHealth].GetComponent<Renderer> ().material = Resources.Load ("Green", typeof(Material)) as Material;
				GlobalVariables.playerOneCurrentHealth++;//add health unit to player 1
			}
		}

		if (collider.tag == "FuelPack") {
			Destroy (collider.gameObject);

			Debug.Log ("Player 1: Recovered Fuel");
			GlobalVariables.currentPlayerFuelLevel = GlobalVariables.playerOneMaxTravelDistance;
			GlobalVariables.currentPlayerFuelText.text = GlobalVariables.currentPlayerFuelLevel.ToString();
		}

		if (collider.tag == "LaserMod") {
			Debug.Log ("Player 1: Equip with laser");
			Destroy (collider.gameObject);
			GlobalVariables.playerOneCurrentAmmoType = "LaserAmmo";
			GlobalVariables.laserStocks = 200;
		}

		if (collider.tag == "AmmoMod") {
			Destroy (collider.gameObject);
			//Randomly assign the player a "mod"
			randomMod = Random.Range(0,2);//[min] is inclusive [max] is exclusive\

			if (randomMod == 0) {
				Debug.Log ("Player 1: BIG EXPLOSION MOD ACTIVATED");
				GlobalVariables.playerOneSizeAOE *= 2;
				GlobalVariables.sizeAOE = GlobalVariables.playerOneSizeAOE;//reload variable

			} 
			else if (randomMod == 1) {
				Debug.Log ("Player 1: ADVANCED FUEL ACTIVATED");
				GlobalVariables.playerOneMaxTravelDistance += 500;
				GlobalVariables.maxTravelDistance = GlobalVariables.playerOneMaxTravelDistance;//reload variable
				GlobalVariables.currentPlayerFuelLevel = GlobalVariables.playerOneMaxTravelDistance;//refuel player
				GlobalVariables.currentPlayerFuelText.text = GlobalVariables.currentPlayerFuelLevel.ToString();
			}
		}
	}
}