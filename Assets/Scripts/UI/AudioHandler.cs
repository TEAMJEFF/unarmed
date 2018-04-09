using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {

	public AudioSource bgm;
	private List<AudioSource> sfx;
	private AudioSource[] initalsound;
	private GameObject[] gameObjects;
	private float bgmVolume;
	private float sfxVolume;

	void Start()
	{

		if (PlayerPrefs.GetFloat ("bgmVolume") == 0) 
		{
			PlayerPrefs.SetFloat ("bgmVolume", 100f);
			PlayerPrefs.Save ();
		}
		if (PlayerPrefs.GetFloat ("sfxVolume") == 0) 
		{
			PlayerPrefs.SetFloat ("sfxVolume", 100f);
			PlayerPrefs.Save ();
		}
	}

	// Get all audio sources
	// Holy shit it gets ALL audio sources...
	void Awake () 
	{
		//Debug.Log (bgm.name);
		sfx = new List<AudioSource> ();
		gameObjects = FindObjectsOfType<GameObject> ();

		for (int i = 0; i < gameObjects.Length; i++) 
		{
			if (gameObjects [i].GetComponents<AudioSource> () != null) 
			{
				initalsound = gameObjects [i].GetComponents<AudioSource> ();
				//Debug.Log (initalsound);
				if (initalsound != null) 
				{
					//Debug.Log (initalsound.Length);
					for (int x = 0; x < initalsound.Length; x++) 
					{
						if (initalsound [x].name != bgm.name) 
						{
							sfx.Add (initalsound [i]);
						}
					}
				}
			}

		}
	}


	public void UpdateVolumes()
	{
		bgmVolume = PlayerPrefs.GetFloat ("bgmVolume");
		sfxVolume = PlayerPrefs.GetFloat ("sfxVolume");
		bgm.volume = bgmVolume;
		for (int i = 0; i < sfx.Count; i++) 
		{
			sfx [i].volume = sfxVolume;
		}
	}

	public void UpdateBgm()
	{
		bgmVolume = PlayerPrefs.GetFloat ("bgmVolume");
		bgm.volume = bgmVolume;
	}
}
