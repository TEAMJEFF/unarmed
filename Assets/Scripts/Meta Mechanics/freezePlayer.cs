using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezePlayer : MonoBehaviour {

	private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl control;
	private GameObject thePlayer;
	private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter character;
	private float timer;
	private bool startFreeze;
	private Rigidbody body;
	private Animator animate;

	// Use this for initialization
	void Start () 
	{
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		control = thePlayer.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl> ();
		character = thePlayer.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter> ();
		body = thePlayer.GetComponent<Rigidbody> ();
		animate = thePlayer.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (startFreeze) 
		{
			body.velocity = new Vector3 (0f, 0f, 0f);
			animate.speed = 0f;
			if (timer < Time.unscaledTime) 
			{
				timer = 0f;
				startFreeze = false;
				control.enabled = true;
				character.enabled = true;
				//body.Sleep ();
			}
		}
	}

	public void freezeOnStart()
	{
		//Debug.Log ("Frozen");
		control.enabled = false;
		character.enabled = false;
		timer = Time.unscaledTime+3f;
		startFreeze = true;
	}

	public void freezeOnPause()
	{
		control.enabled = false;
		character.enabled = false;
		timer = Time.unscaledTime + 1.5f;
		startFreeze = true;
	}

}
