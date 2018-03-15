using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class restartButton : PausingScript {

	public Button button;
	// Use this for initialization
	void Start () 
	{
		Button btn = button.GetComponent<Button> ();
		btn.onClick.AddListener (ifClicked);
	}

	void ifClicked()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		unPause ();
	}
}
