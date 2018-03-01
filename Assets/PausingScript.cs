using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausingScript : MonoBehaviour {

	[SerializeField] Camera mainCam;
	[SerializeField] Camera pauseCam;
	private bool isPaused = false;


	// Use this for initialization
	void Start () 
	{
		mainCam.GetComponent<Camera> ();
		mainCam.enabled = true;
		pauseCam.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			if (!isPaused) {
//				Debug.Log ("NOW PAUSED");
//				isPaused = true;
//				Time.timeScale = 0f;
//				mainCam.enabled = false;
//				pauseCam.enabled = true;
				pause();
			} else {
//				Debug.Log ("UNPAUSED");
//				isPaused = false;
//				Time.timeScale = 1f;
//				mainCam.enabled = true;
//				pauseCam.enabled = false;
				unPause();
			}
		}
		if(Time.timeScale == 1f)
		{
			isPaused = false;
		}
	}
	public void unPause()
	{
		Debug.Log ("UNPAUSED");
		isPaused = false;
		Time.timeScale = 1f;
		mainCam.enabled = true;
		pauseCam.enabled = false;
	}

	public void pause()
	{
		Debug.Log ("NOW PAUSED");
		isPaused = true;
		Time.timeScale = 0f;
		mainCam.enabled = false;
		pauseCam.enabled = true;
	}
}
