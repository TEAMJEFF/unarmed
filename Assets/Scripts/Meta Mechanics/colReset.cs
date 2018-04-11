using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class colReset : MonoBehaviour
{
	public float resetSeconds;	
	private float resetTime;
	private int SceneNumber;
	private bool isHit = false;
	private Scene theScene;
	private ScreenFader fadeScr;


	void Awake()
	{
		fadeScr = GameObject.FindObjectOfType<ScreenFader>();
		theScene = SceneManager.GetActiveScene ();
		SceneNumber = theScene.buildIndex;
		isHit = false;
	}

	// In collision for certain period reset
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.layer == 9)  
		{
			//Debug.Log ("It hit");
			if (!isHit) 
			{
				resetTime = Time.time + resetSeconds;
				isHit = true;
			} 
		}
		if (col.gameObject.layer == 10)  
		{
			//Debug.Log ("It hit");
			if (!isHit) 
			{
				resetTime = Time.time + resetSeconds;
				isHit = true;
			} 
		}
			

		//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.layer == 9) 
		{
			isHit = false;
		}
		//Debug.Log ("It Unhit");
		//isHit = false;
		if (col.gameObject.layer == 10) 
		{
			isHit = false;
		}
	}

	void Update()
	{
		if (isHit) 
		{
			if (resetTime < Time.time) 
			{
				isHit = false;
                //fadeScr.BoundsRestart (SceneNumber);
                fadeScr.RestartCheckpoint();
			}
		}
	}
}