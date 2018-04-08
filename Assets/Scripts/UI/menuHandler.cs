using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuHandler : MonoBehaviour {

	// Use this for initialization
	public GameObject mainButtons;
	public GameObject optionButtons;

	public Button optionButton;
	public Button goBack;

	void Start () 
	{
		optionButton.onClick.AddListener (optionClick);
		goBack.onClick.AddListener (backClick);
	}

	void optionClick()
	{
		mainButtons.SetActive (false);
		optionButtons.SetActive (true);
	}

	void backClick()
	{
		optionButtons.SetActive (false);
		mainButtons.SetActive (true);
	}
}
