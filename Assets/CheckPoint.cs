using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    private Vector3 spawnPoint;
    private Quaternion spawnRotation;


	// Use this for initialization
	void Awake()
    {
        spawnPoint = transform.position;
        spawnRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Respawn")
        {
            Debug.Log("I am dah best");
            spawnPoint = gameObject.transform.position;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Respawn")
        {
            Debug.Log("HIT");
            spawnPoint = transform.position;
        }
    }

    public void resetCharacter()
    {
        gameObject.transform.position = spawnPoint;
        gameObject.transform.rotation = spawnRotation;
    }


}
