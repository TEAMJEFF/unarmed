using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

	public GameObject wall;
    public GameObject cam;

	public bool leverPulled = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (leverPulled) {
            
		}
	}

	void PulledDown() {
		leverPulled = true;
        //GetComponent<CameraShake>().enabled = true;
        //cam.GetComponent<ThirdPersonCamera>().enabled = false;
        //cam.GetComponent<Rigidbody>().AddExplosionForce(500f, transform.position, 1f, 100f);
    }
}
