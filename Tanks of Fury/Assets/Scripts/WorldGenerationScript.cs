using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerationScript : MonoBehaviour {

	//Array to store terrain objects
	[SerializeField]
	private GameObject [] trees;
	//[SerializeField]
	//private GameObject[] stones;

	[SerializeField]
	private GameObject[] terrain;

	[SerializeField]
	private GameObject[] powerUp;


	public int stoneChances = 2 ;

	[SerializeField]
	private GameObject blMarker;
	[SerializeField]
	private GameObject trMarker;

	//Grid position
	private Vector3 currentPosition;
	private Vector3 worldObjectStartPos;
	private Vector3 terrainStartPos;

	private float groundWidth;

	private float worldObjectIncAmount;
	private float terrainIncAmount;

	private float worldObjectRandAmount;
	private float terrainRandAmount;
	 
	[SerializeField]
	private int worldObjectRowsCols;
	[SerializeField]
	private int terrainRowsCols;

	//number of cycles and current cycles the loop is generating
	[SerializeField]
	private int repeatCycle;
	private int currentGeneration;

	[SerializeField]
	private float worldObjectSphereRadius;
	[SerializeField]
	private float terrainRadius;

	[SerializeField]
	private LayerMask groundLayer;
	[SerializeField]
	private LayerMask terrainLayer;
	[SerializeField]
	private LayerMask worldObjectLayer;


	// Use this for initialization
	void Start () {
		StartCoroutine ("PopulateWorld");
	}
	
	IEnumerator PopulateWorld(){
		groundWidth = trMarker.transform.position.x - blMarker.transform.position.x;

		worldObjectIncAmount = groundWidth / worldObjectRowsCols;
		worldObjectRandAmount = worldObjectIncAmount / 2f;

		terrainIncAmount = groundWidth / worldObjectRowsCols;
		terrainRandAmount = terrainIncAmount / 2f;

		worldObjectStartPos = new Vector3 (blMarker.transform.position.x - (worldObjectIncAmount/2f), blMarker.transform.position.y,
			blMarker.transform.position.z + (worldObjectIncAmount/2f));
		
		terrainStartPos = new Vector3 (blMarker.transform.position.x - (terrainIncAmount / 2f), blMarker.transform.position.y, 
			blMarker.transform.position.z + (terrainIncAmount / 2f));


		for (int repeat = 0; repeat <= repeatCycle; repeat++) {
			currentGeneration = repeat;

			if (currentGeneration == 0) {
				currentPosition = terrainStartPos;

				for (int cols = 1; cols <= terrainRowsCols; cols++) {
					for (int rows = 1; rows <= terrainRowsCols; rows++) {
						currentPosition = new Vector3 (currentPosition.x + terrainIncAmount, currentPosition.y, currentPosition.z);
						GameObject newSpawn = terrain [Random.Range (0, terrain.Length)];

						spawnHere (currentPosition, newSpawn, terrainRadius, true);

						yield return new WaitForSeconds (0.1f);
					}

					currentPosition = new Vector3 (terrainStartPos.x, currentPosition.y, currentPosition.z + terrainIncAmount); 
				}
			} 
			else if (currentGeneration > 0) {
				currentPosition = worldObjectStartPos;

				for (int cols = 1; cols <= worldObjectRowsCols; cols++) {
					for (int rows = 1; rows <= worldObjectRowsCols; rows++) {
						currentPosition = new Vector3 (currentPosition.x + worldObjectIncAmount, currentPosition.y, currentPosition.z);

						int spawnChance = Random.Range (1, stoneChances + 1);

						if (spawnChance == 1) {
							GameObject newSpawn = powerUp [Random.Range (0, powerUp.Length)];
							spawnHere (currentPosition, newSpawn, worldObjectSphereRadius, false);

							yield return new WaitForSeconds (0.1f);

						}


						if (spawnChance > 1) {
							GameObject newSpawn = trees [Random.Range (0, trees.Length)];
							spawnHere (currentPosition, newSpawn, worldObjectSphereRadius, false);

							yield return new WaitForSeconds (0.1f);
						}
					}

					currentPosition = new Vector3 (worldObjectStartPos.x, currentPosition.y, currentPosition.z + worldObjectIncAmount);
				}
			}
		}
		worldGenDone ();
	}

	void spawnHere (Vector3 newSpawnPos, GameObject objectToSpawn, float radiusOfSphere, bool isObjectTerrrain){
		if (isObjectTerrrain == true) {
			Vector3 randPos = new Vector3 (newSpawnPos.x + Random.Range (-terrainRandAmount, terrainRandAmount + 1), 0, newSpawnPos.z +
			                  Random.Range (-terrainRandAmount, terrainRandAmount + 1));
			Vector3 rayPos = new Vector3 (randPos.x, 10, randPos.z);

			if (Physics.Raycast (rayPos, -Vector3.up, Mathf.Infinity, groundLayer)) {
				Collider[] objectsHit = Physics.OverlapSphere (randPos, radiusOfSphere, terrainLayer);

				if (objectsHit.Length == 0) {
					GameObject terrainObject = (GameObject)Instantiate (objectToSpawn, randPos, Quaternion.identity);

					terrainObject.transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Random.Range (0, 360), transform.eulerAngles.z);
				}
			}
		} 
		else {
			Vector3 randPos = new Vector3 (newSpawnPos.x + Random.Range (-worldObjectRandAmount, worldObjectRandAmount + 1), newSpawnPos.y, 
				                  newSpawnPos.z + Random.Range (-worldObjectRandAmount, worldObjectRandAmount + 1));
			Vector3 rayPos = new Vector3 (randPos.x, 20, randPos.z);
			RaycastHit hit;

			if (Physics.Raycast (rayPos, -Vector3.up, out hit, Mathf.Infinity, groundLayer)) {
				randPos = new Vector3 (randPos.x, hit.point.y, randPos.z);

				Collider[] objectsHit = Physics.OverlapSphere (randPos, radiusOfSphere, worldObjectLayer);

				if (objectsHit.Length == 0) {
					//Puts the terrain object slightly below the ground
					GameObject worldObject = (GameObject)Instantiate (objectToSpawn, randPos, Quaternion.identity);
					worldObject.transform.position = new Vector3 (worldObject.transform.position.x, worldObject.transform.position.y +
						(worldObject.GetComponent<Renderer> ().bounds.extents.y * 0.6f), worldObject.transform.position.z);
					worldObject.transform.eulerAngles = new Vector3 (transform.eulerAngles.x, Random.Range (0, 360), transform.eulerAngles.z); 
				}
			}
		}
	}

	void worldGenDone(){
		print ("World is done generated");
	}
}
