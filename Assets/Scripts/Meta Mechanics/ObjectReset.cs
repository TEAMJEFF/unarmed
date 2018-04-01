using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class ObjectReset : MonoBehaviour {

    private GameObject[] gameObjects;
    private List<ResetClass> resets;
    public GameObject FPC;

    private void Start()
    {
        FPC = GameObject.Find("FPSController");
    }
    // Use this for initialization
    // ONLY DONE ONCE 
    void Awake()
    {
        resets = new List<ResetClass>();
        gameObjects = FindObjectsOfType<GameObject>();
        for( int i = 0; i < gameObjects.Length; i++)
        {
            if(gameObjects[i].layer == 10)
            {
                resets.Add(new ResetClass(gameObjects[i], gameObjects[i].transform.position, gameObjects[i].transform.rotation));
            }
			if (gameObjects [i].layer == 1) 
			{
				resets.Add(new ResetClass(gameObjects[i], gameObjects[i].transform.position, gameObjects[i].transform.rotation));
			}
        }
	}

    // So like i hope it does the thing
    public void PleaseReset()
    {
        for (int i = 0; i < resets.Count; i++)
        {
            resets[i].ResetThePositions();
        }
        FPC.GetComponent<CameraSightline>().ResetTransparency();
    }
	
}
