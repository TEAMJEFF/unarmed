using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class colReset : MonoBehaviour
{
	public float resetSeconds;	
	private float resetTime;
	private int SceneNumber;
	private bool isHit;
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
		if (col.gameObject.layer == 0) 
		{
			//Debug.Log ("It hit");
			if (!isHit) 
			{
				resetTime = Time.time + resetSeconds;
				isHit = true;
			} 
//			else 
//			{
//				Debug.Log ("Right before time check");
//				if (resetTime < Time.time) 
//				{
//					isHit = false;
//					fadeScr.BoundsRestart (SceneNumber);
//				}
//			}
		}
			

		//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.layer == 0) 
		{
			isHit = false;
		}
		//Debug.Log ("It Unhit");
		//isHit = false;
	}

	void Update()
	{
		if (isHit) 
		{
			if (resetTime < Time.time) 
			{
				isHit = false;
				fadeScr.BoundsRestart (SceneNumber);
			}
		}
	}
}