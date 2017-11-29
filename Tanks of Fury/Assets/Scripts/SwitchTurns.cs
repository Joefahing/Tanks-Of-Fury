using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchTurns : MonoBehaviour {
	public Camera playerOneCameraLINK;
	public GameObject playerOneTankLINK;
	public GameObject playerOneTurretLINK;
	public GameObject playerOneCannonLINK;
	public GameObject playerOneMenuLINK;
	public TextMesh playerOneFuelLevelLINK;

	public Camera playerTwoCameraLINK;
	public GameObject playerTwoTankLINK;
	public GameObject playerTwoTurretLINK;
	public GameObject playerTwoCannonLINK;
	public GameObject playerTwoMenuLINK;
	public TextMesh playerTwoFuelLevelLINK;

	void Start() {
		//linking passed in variables from the Unity UI to the Global Static ones. This
		//looks ugly, but its the only way Unity can link static variables through the UI
		GlobalVariables.playerOneCamera = playerOneCameraLINK;
		GlobalVariables.playerOneTank = playerOneTankLINK;
		GlobalVariables.playerOneTurret = playerOneTurretLINK;
		GlobalVariables.playerOneCannon = playerOneCannonLINK;
		GlobalVariables.playerOneMenu = playerOneMenuLINK;
		GlobalVariables.playerOneFuelLevel = playerOneFuelLevelLINK;

		GlobalVariables.playerTwoCamera = playerTwoCameraLINK;
		GlobalVariables.playerTwoTank = playerTwoTankLINK;
		GlobalVariables.playerTwoTurret = playerTwoTurretLINK;
		GlobalVariables.playerTwoCannon = playerTwoCannonLINK;
		GlobalVariables.playerTwoMenu = playerTwoMenuLINK;
		GlobalVariables.playerTwoFuelLevel = playerTwoFuelLevelLINK;

		//player 1 starts the game, so end player 2s turn
		GlobalVariables.CurrentPlayerTurn = 2;
		switchPlayersTurns();
	}

	public static void switchPlayersTurns() {
		if (GlobalVariables.CurrentPlayerTurn == 1) {
			//end player ones turn, start player twos turn
			updateCurrentPlayer (GlobalVariables.playerTwoTank,GlobalVariables.playerTwoTurret,GlobalVariables.playerTwoCannon,GlobalVariables.playerTwoFuelLevel,false,true,2,GlobalVariables.playerTwoMaxTravelDistance,GlobalVariables.playerTwoSizeAOE);
		}
		else if (GlobalVariables.CurrentPlayerTurn == 2) {
			//end player twos turn, start player ones turn
			updateCurrentPlayer (GlobalVariables.playerOneTank,GlobalVariables.playerOneTurret,GlobalVariables.playerOneCannon,GlobalVariables.playerOneFuelLevel,true,false,1,GlobalVariables.playerOneMaxTravelDistance,GlobalVariables.playerOneSizeAOE);
		}
	}

	public static void updateCurrentPlayer(GameObject tank, GameObject turret, GameObject cannon,TextMesh fuel,bool playerOne, bool playerTwo, int player, int playerMaxTravelDistance, int playerSizeAOE) {
		//toggle Player 1 and Player 2s camera and menu
		GlobalVariables.playerTwoMenu.SetActive(playerTwo);//hide Player 1s Menu
		GlobalVariables.playerTwoCamera.enabled = playerTwo;//deactivate Player 2s Camera

		GlobalVariables.playerOneMenu.SetActive(playerOne);//hide Player 1s Menu
		GlobalVariables.playerOneCamera.enabled = playerOne;//enable Player 1s Camera

		//update global variables that each user may have changed
		GlobalVariables.maxTravelDistance = playerMaxTravelDistance; 
		GlobalVariables.sizeAOE = playerSizeAOE; 

		//update current player variables
		GlobalVariables.currentPlayerTank = tank;
		GlobalVariables.currentPlayerTurret = turret;
		GlobalVariables.currentPlayerCannon = cannon;
		GlobalVariables.CurrentPlayerTurn = player;
		GlobalVariables.currentPlayerFuelText = fuel;
		GlobalVariables.currentPlayerFuelText.text = GlobalVariables.maxTravelDistance.ToString ();
		GlobalVariables.currentPlayerStartingLocation = GlobalVariables.currentPlayerTank.transform.position;
		GlobalVariables.currentPlayerFuelLevel = GlobalVariables.maxTravelDistance;
		GlobalVariables.currentPlayerMaxTravelDistance = false;
		GlobalVariables.currentPlayerHasFiredCannon = false;
	}
}