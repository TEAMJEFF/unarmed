using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour {

	public Button button;
	// Use this for initialization
	void Start () 
	{
		
		Button btn = button.GetComponent<Button> ();
		btn.onClick.AddListener (ifClicked);
	}
	
	void ifClicked()
	{
		//Debug.Log ("Clicked");
		Application.Quit();
	}
}
