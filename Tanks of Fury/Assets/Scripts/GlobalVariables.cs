using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalVariables : MonoBehaviour {
	//global constant variables that any script can access
	public static int moveSpeed = 25;
	public static int jumpSpeed = 350;
	public static int cannonMultiplier = 1000;
	public static float speedAOE = 0.1f;
	public static int fuelMultiplier = 5;


	//Ammo mods where players can change these values for themselves
	public static int maxTravelDistance = 1000;
	public static float sizeAOE;

	//Player 1
	public static int playerOneCurrentHealth;
	public static int playerOneMaxHealth;
	public static Camera playerOneCamera;
	public static GameObject playerOneTank;
	public static GameObject playerOneTurret;
	public static GameObject playerOneCannon;
	public static GameObject playerOneMenu;
	public static TextMesh playerOneFuelLevel;

	//Type of ammo player one is currently using
	public static string playerOneCurrentAmmoType = "NormalAmmo";
	public static int laserStocks = 0;

	public static int playerOneSizeAOE = 10;
	public static int playerOneMaxTravelDistance = 1000;

	//Player 2
	public static int playerTwoCurrentHealth;
	public static int playerTwoMaxHealth;
	public static Camera playerTwoCamera;
	public static GameObject playerTwoTank;
	public static GameObject playerTwoTurret;
	public static GameObject playerTwoCannon;
	public static GameObject playerTwoMenu;
	public static TextMesh playerTwoFuelLevel;

	public static int playerTwoSizeAOE = 10;
	public static int playerTwoMaxTravelDistance = 1000;

	//Current Players Turn Game Objects for 
	public static int CurrentPlayerTurn;
	public static int currentPlayerFuelLevel;
	public static bool currentPlayerMaxTravelDistance;
	public static bool currentPlayerHasFiredCannon;
	public static Vector3 currentPlayerStartingLocation;
	public static GameObject currentPlayerTank;
	public static GameObject currentPlayerTurret;
	public static GameObject currentPlayerCannon;
	public static TextMesh currentPlayerFuelText;

	public static void fireCannon(Vector3 spawnLocation, Vector3 forceApplied) {
		currentPlayerMaxTravelDistance = true;//prevent player from moving after shooting
		currentPlayerHasFiredCannon = true;

		GameObject newBullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		newBullet.transform.localScale = new Vector3(.5f,.5f,.5f);
		newBullet.transform.position = spawnLocation;

		newBullet.GetComponent<Renderer> ().material = Resources.Load ("Bullet", typeof(Material)) as Material;
		newBullet.AddComponent(System.Type.GetType("BulletExplosion"));

		newBullet.AddComponent<Rigidbody>();
		newBullet.GetComponent<Rigidbody>().AddForce(forceApplied);

		//SwitchTurns.switchPlayersTurns ();


	}

	public static void fireLazer(Vector3 spawnLocation, Vector3 forceApplied){

		if (laserStocks == 0) {
			currentPlayerHasFiredCannon = true;
			playerOneCurrentAmmoType = "Normal";
			SwitchTurns.switchPlayersTurns();
		}

		currentPlayerMaxTravelDistance = true;


		GameObject newBullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		newBullet.GetComponent<Collider> ().isTrigger = true;
		newBullet.gameObject.tag = "AOEDamage";
	
		newBullet.transform.localScale = new Vector3(.5f,.5f,.5f);
		newBullet.transform.position = spawnLocation;

		newBullet.GetComponent<Renderer> ().material = Resources.Load ("Bullet", typeof(Material)) as Material;
		newBullet.AddComponent(System.Type.GetType("BulletExplosion"));

		newBullet.AddComponent<Rigidbody>();
		newBullet.GetComponent<Rigidbody> ().useGravity = false;
		newBullet.GetComponent<Rigidbody>().AddForce(forceApplied);

	}
}