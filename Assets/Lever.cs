using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

	public GameObject LeftWall;
	public GameObject RightWall;

	public bool leverPulled = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (leverPulled) {
			LeftWall = GameObject.Find("Left Wall");
			RightWall = GameObject.Find("Right Wall");

			LeftWall.transform.position = Vector3.MoveTowards(LeftWall.transform.position, transform.position, (3.0f * Time.deltaTime));
			RightWall.transform.position = Vector3.MoveTowards(RightWall.transform.position, transform.position, (3.0f * Time.deltaTime));
		}
	}

	void PulledDown() {
		leverPulled = true;
	}
}
