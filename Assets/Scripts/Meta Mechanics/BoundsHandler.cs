﻿// based off of https://gist.github.com/NovaSurfer/5f14e9153e7a2a07d7c5
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Added to the character for boundary checking
// Resets character if they go too far

public class BoundsHandler : MonoBehaviour
{

    ScreenFader fadeScr;
    GameObject thePlayer;
    Scene thisScene;
    public int SceneNumb;
    public float MAX_X;
    public float MIN_Y;
    public float MIN_X;
	public float resetSeconds;
    public float LOW_Y; // used for reset
	private float resetTime;
	private bool isOut;
    //private float MAX_X = 525f;
    //private float MIN_X = 510f;
    //private float MIN_Y = 30f;


    void Awake()
    {
        fadeScr = FindObjectOfType<ScreenFader>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        thisScene = SceneManager.GetActiveScene();
        SceneNumb = thisScene.buildIndex;
		isOut = false;
    }

    // Update checks bounds
    void Update()
    {
        //Debug.Log(thePlayer.transform.position.x);
        //Debug.Log(thePlayer.transform.position.y);
		if (thePlayer.transform.position.x > MAX_X | thePlayer.transform.position.x < MIN_X) 
		{
			if (thePlayer.transform.position.y < MIN_Y) 
			{
				// Out of bounds start timer or continue timer 
				if (!isOut) 
				{
					resetTime = Time.time + resetSeconds;
					isOut = true;
				} 
				else 
				{
					if (resetTime < Time.time) 
					{
						isOut = false;
                        //fadeScr.BoundsRestart (SceneNumb);
                        fadeScr.RestartCheckpoint();
					}
				}
				//fadeScr.BoundsRestart (SceneNumb);
				//Debug.Log("IN RESTART ZONE");
			}
		} 
		else 
		{
			// If no longer out of bounds reset
			if (isOut) 
			{	
				isOut = false;
			}
		}

        // Lastly check to make sure not below certain Y
        if(thePlayer.transform.position.y < LOW_Y)
        {
            //fadeScr.BoundsRestart(SceneNumb);
            //fadeScr.RestartCheckpoint();
            fadeScr.fallReset();
        }
    }
   /*
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            fadeScr.EndScene(SceneNumb);
        }
    }
    */

    
}