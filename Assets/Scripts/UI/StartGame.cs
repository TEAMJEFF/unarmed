﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
	public Button button;
    public ScreenFader screenFader;

    // Use this for initialization
    void Start ()
	{

		Button btn = button.GetComponent<Button> ();
		btn.onClick.AddListener (onClick);
	}
	
	void onClick()
	{
        //SceneManager sceneManager;		
        //SceneManager.LoadScene("Cutscene1", LoadSceneMode.Single);
        screenFader.EndScene(1);
	}
}

