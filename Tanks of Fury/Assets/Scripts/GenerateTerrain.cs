using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//3 layers, check newly created spheres for start and end coordinates
		//look at destructable spheres for 

		for(int x = -50; x <= 50; x++) {
			for(int z = -50; z <= 50; z++) {
				//create cube at location of X,Y,Z with a RigidBody and a Tag = "Terrain"
				GameObject newTerrainCube = GameObject.CreatePrimitive(PrimitiveType.Quad);
				newTerrainCube.transform.localScale = new Vector3 (1, 0, 1);
				newTerrainCube.transform.Rotate(new Vector3 (90, 0, 0));
				newTerrainCube.tag = "Terrain";
				newTerrainCube.isStatic = true;
				newTerrainCube.transform.position = new Vector3(x,0,z);
				newTerrainCube.GetComponent<Renderer> ().material = Resources.Load ("Terrain", typeof(Material)) as Material;
				newTerrainCube.AddComponent<Rigidbody>();
				newTerrainCube.GetComponent<Rigidbody> ().isKinematic = true;
				newTerrainCube.GetComponent<Rigidbody> ().useGravity = false;

			}
		}
	}
}