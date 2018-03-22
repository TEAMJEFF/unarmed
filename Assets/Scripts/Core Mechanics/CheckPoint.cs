using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Checkpoint is added to the character 
// Updates checkpoints 

public class CheckPoint : MonoBehaviour {

    private Vector3 spawnPoint;
    private Quaternion spawnRotation;
    public Text checkpointText;
    private bool onHit;
    private float timer;


	// Use this for initialization
	void Awake()
    {
        spawnPoint = transform.position;
        spawnRotation = transform.rotation;
        checkpointText.enabled = false;
        onHit = false;
        timer = 0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(onHit)
        {
            if(timer != 0f)
            {
                if (Time.fixedTime > timer + 2f)
                {
                    checkpointText.enabled = false;
                    timer = 0f;
                    onHit = false;
                }
            }
        }
	}

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Respawn")
        {
            Debug.Log("I am dah best");
            onHit = true;
            timer = Time.fixedTime;
            spawnPoint = gameObject.transform.position;
            checkpointText.enabled = true;
        }
    }

    public void resetCharacter()
    {
        gameObject.transform.position = spawnPoint;
        gameObject.transform.rotation = spawnRotation;
    }


}
