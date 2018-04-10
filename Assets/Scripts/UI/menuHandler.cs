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

	public Slider bgmSliderVol;
	public Slider sfxSliderVol;

	public AudioHandler audioHandler;

	void Start () 
	{
		optionButton.onClick.AddListener (optionClick);
		goBack.onClick.AddListener (backClick);
		bgmSliderVol.onValueChanged.AddListener (delegate {bgmVolumeChange();});
		sfxSliderVol.onValueChanged.AddListener (delegate {sfxVolumeChange();});

	}

	void optionClick()
	{
		mainButtons.SetActive (false);
		updateSliders ();
		optionButtons.SetActive (true);
	}

	void backClick()
	{
		optionButtons.SetActive (false);
		mainButtons.SetActive (true);
	}

	void updateSliders()
	{
		bgmSliderVol.value = PlayerPrefs.GetFloat ("bgmVolume");
		sfxSliderVol.value = PlayerPrefs.GetFloat ("sfxVolume");
	}

	void bgmVolumeChange()
	{
		PlayerPrefs.SetFloat ("bgmVolume", bgmSliderVol.value);
		audioHandler.UpdateBgm ();
		PlayerPrefs.Save ();
	}

	void sfxVolumeChange()
	{
		PlayerPrefs.SetFloat ("sfxVolume", sfxSliderVol.value);
		PlayerPrefs.Save ();
	}
}
