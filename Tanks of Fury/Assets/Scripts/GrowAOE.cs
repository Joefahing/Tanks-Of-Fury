using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowAOE : MonoBehaviour {
	float localAOE;

	void Start () {
		localAOE = 1;//inital size
	}

	void Update () {
		localAOE += GlobalVariables.speedAOE;

		if(transform.localScale.x < GlobalVariables.sizeAOE) {
			//grow the AOE until it hits a specific size and then destroy it
			transform.localScale = new Vector3(1,1,1) * localAOE;
		}	
		else {
			Destroy(gameObject);//animation has played out, therefore delete it
			//switch players turns now
			SwitchTurns.switchPlayersTurns();
		}
	}

	void OnTriggerEnter(Collider collider) {
		if (collider.tag == "Terrain" || collider.tag == "WorldObject") {
			//Debug.Log("Destroy Object that this AOE came into contact with");
			Destroy (collider.gameObject);
		}
	}
}