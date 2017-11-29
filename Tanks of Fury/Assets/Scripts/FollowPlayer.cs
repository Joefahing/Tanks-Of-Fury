using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	public GameObject playerCube;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - playerCube.transform.position;
	}

	// Update is called once per frame
	void Update () {
		transform.position = playerCube.transform.position + offset;
	}
}