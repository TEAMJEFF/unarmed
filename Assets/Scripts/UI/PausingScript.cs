using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausingScript : MonoBehaviour {

	[SerializeField] GameObject player;
	[SerializeField] Camera mainCam;
	[SerializeField] Camera pauseCam;

	public CanvasGroup UICanvas;
	public bool isPaused = false;
	private freezePlayer freeze;
	private GameObject thePlayer;
	MonoBehaviour unit;

	void Awake()
	{
		thePlayer = GameObject.FindGameObjectWithTag ("Player");
		freeze = thePlayer.GetComponent<freezePlayer> ();
		//Debug.Log (thePlayer.name);
	}


	// Use this for initialization
	void Start () 
	{
		//unit = player.GetComponent("GrapplingHook") as MonoBehaviour;
		mainCam.GetComponent<Camera> ();
		mainCam.enabled = true;
		pauseCam.enabled = false;
		//freeze = GameObject.FindGameObjectWithTag ("Player").GetComponent<freezePlayer> ();
		//thePlayer = GameObject.FindGameObjectWithTag("Player");
		//freeze = thePlayer.GetComponent<freezePlayer>();

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
				//MonoBehaviour unit = (GetComponent("GrapplingHook") as MonoBehaviour);
				//unit.enabled = false;
				pause();
			} else {
				//				Debug.Log ("UNPAUSED");
				//				isPaused = false;
				//				Time.timeScale = 1f;
				//				mainCam.enabled = true;
				//				pauseCam.enabled = false;
				//unit.enabled = true;
				unPause();
			}
		}
		if(Time.timeScale == 1f)
		{
			//unit.enabled = true;
			isPaused = false;
		}
	}
	public void unPause()
	{
		Debug.Log ("UNPAUSED");
		isPaused = false;
		UICanvas.blocksRaycasts = false;
		UICanvas.interactable = false;

		Time.timeScale = 1f;
		mainCam.enabled = true;
		pauseCam.enabled = false;
		freeze.freezeOnPause ();
	}

	public void pause()
	{
		Debug.Log ("NOW PAUSED");
		isPaused = true;
		UICanvas.blocksRaycasts = true;
		UICanvas.interactable = true;

		Time.timeScale = 0f;
		mainCam.enabled = false;
		pauseCam.enabled = true;
	}
}