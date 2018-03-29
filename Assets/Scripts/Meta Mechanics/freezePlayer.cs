using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class freezePlayer : MonoBehaviour {

	private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl control;
	private GameObject thePlayer;
	private UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter character;
	private float timer;
	private bool startFreeze;
	private bool inDefFreeze;
	private Rigidbody body;
	private Animator animate;
	private RepelBlast stopBlast;

	public Text clickToClose;

	// Use this for initialization
	void Start () 
	{
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		control = thePlayer.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl> ();
		character = thePlayer.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter> ();
		body = thePlayer.GetComponent<Rigidbody> ();
		animate = thePlayer.GetComponent<Animator> ();
		stopBlast = thePlayer.GetComponent<RepelBlast> ();
		clickToClose.enabled = false;
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
				stopBlast.enabled = true;
				//body.Sleep ();
			}
		}
		if (inDefFreeze) 
		{	
			body.velocity = new Vector3 (0f, 0f, 0f);
			animate.speed = 0f;
			if (timer < Time.unscaledTime) 
			{
				//Debug.Log ("Time has passed");
				clickToClose.enabled = true;
				if(Input.GetButtonDown("Fire1"))
				{
					clickToClose.enabled = false;	
					control.enabled = true;
					character.enabled = true;
					stopBlast.enabled = true;
					inDefFreeze = false;
					timer = 0f;
					freezeOnStart (1f);
				}
			}

		}
	}

	public void freezeOnStart(float aTime = 2f)
	{
		//Debug.Log ("Frozen");
		control.enabled = false;
		stopBlast.enabled = false;
		character.enabled = false;
		timer = Time.unscaledTime+aTime;
		startFreeze = true;
	}

	public void freezeOnPause(float aTime = 1.5f)
	{
		control.enabled = false;
		character.enabled = false;
		timer = Time.unscaledTime + aTime;
		startFreeze = true;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "GameController") 
		{
			control.enabled = false;
			character.enabled = false;
			inDefFreeze = true;
			stopBlast.enabled = false;
			timer = Time.unscaledTime + 2f;
		}
	}

}
