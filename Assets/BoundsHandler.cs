// based off of https://gist.github.com/NovaSurfer/5f14e9153e7a2a07d7c5
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BoundsHandler : MonoBehaviour
{

    ScreenFader fadeScr;
    GameObject thePlayer;
    Scene thisScene;
    public int SceneNumb;
    public float MAX_X;
    public float MIN_Y;
    public float MIN_X;
    //private float MAX_X = 525f;
    //private float MIN_X = 510f;
    //private float MIN_Y = 30f;


    void Awake()
    {
        fadeScr = GameObject.FindObjectOfType<ScreenFader>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        thisScene = SceneManager.GetActiveScene();
        SceneNumb = thisScene.buildIndex;
    }

    // Update checks bounds
    void Update()
    {
        //Debug.Log(thePlayer.transform.position.x);
        if(thePlayer.transform.position.x > MAX_X | thePlayer.transform.position.x < MIN_X)
        {
            if(thePlayer.transform.position.y < MIN_Y)
            {
                fadeScr.BoundsRestart(SceneNumb);
                //Debug.Log("IN RESTART ZONE");
            }
        }
    }
   /*
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            fadeScr.EndScene(SceneNumb);
        }
    }
    */

    
}