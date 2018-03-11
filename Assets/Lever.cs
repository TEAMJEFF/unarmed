using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour {

	public GameObject platform;
    public GameObject handle;
    private Vector3 endPos;

	public bool leverPulled = false;

	// Use this for initialization
	void Start () {
        endPos = platform.transform.position + new Vector3(0, 13.7f, -5);
	}
	
	// Update is called once per frame
	void Update () {
		if (leverPulled) {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, endPos, 10 * Time.deltaTime);
		}
	}

	void PulledDown() {
		leverPulled = true;
        handle.GetComponent<Rigidbody>().AddForce(0, 0, -20f);
        
        //GetComponent<CameraShake>().enabled = true;
        //cam.GetComponent<ThirdPersonCamera>().enabled = false;
        //cam.GetComponent<Rigidbody>().AddExplosionForce(500f, transform.position, 1f, 100f);
    }
}
