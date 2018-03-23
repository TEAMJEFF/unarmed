// Based off of https://gist.github.com/NovaSurfer/5f14e9153e7a2a07d7c5
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

// HOW TO USE SCREEN FADE
// Duplicate Fill from HUDcanvas and keep it in the canvas 
// Delete everything in it except fill image
// Change fill image name to fade and make color black 
// It works...? 

public class ScreenFader : MonoBehaviour
{
    public Image FadeImg;
    public float fadeSpeed;
    public bool sceneStarting = true;
    private GameObject thePlayer;
    private CheckPoint checkPoint;
    private ObjectReset objectReset;





    void Awake()
    {
		if (FadeImg != null) 
		{
			FadeImg.rectTransform.localScale = new Vector2 (Screen.width, Screen.height);
			FadeImg.enabled = true;
		}
    }

    // Start happens after awake. So everything will be set by now
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        // Gets the player objects checkpoint info
        // Gets objects list
        checkPoint = thePlayer.GetComponent<CheckPoint>();
        objectReset = thePlayer.GetComponent<ObjectReset>();
    }
		

    void Update()
    {
        // If the scene is starting...
        if (sceneStarting)
            // ... call the StartScene function.
            StartScene();
    }


    void FadeToClear()
    {
        // Lerp the colour of the image between itself and transparent.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void newClear()
    {
        FadeImg.color = Color.clear;
        FadeImg.enabled = false;
    }

    void FadeToBlack()
    {

        //Debug.Log("THIS HAPPENS");
        // Lerp the colour of the image between itself and black.
        FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
    }


    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

        // If the texture is almost clear...
        if (FadeImg.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the RawImage.
            FadeImg.color = Color.clear;
            FadeImg.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
        }
    }

    public IEnumerator BoundsRestartRoutine(int SceneNumber)
    {
        //Debug.Log("HERE");
        FadeImg.enabled = true;
        //Time.timeScale = 0f;
        do
        {
            FadeToBlack();

            if (FadeImg.color.a >= 0.99f)
            {
                SceneManager.LoadScene(SceneNumber);
                yield break;
            }
            else
            {
                yield return null;
            }
        } while (true);
    }

    public void BoundsRestart(int SceneNumber)
    {
        sceneStarting = false;
        StartCoroutine("BoundsRestartRoutine", SceneNumber);
    }

    public IEnumerator RestartCheckpointRoutine()
    {
        FadeImg.enabled = true;
        do
        {
            FadeToBlack();

            if (FadeImg.color.a >= 0.99f)
            {
                objectReset.PleaseReset();
                checkPoint.resetCharacter();
                newClear();
                yield break;
            }
            else
            {
                yield return null;
            }
        } while (true);
    }

    public void RestartCheckpoint()
    {
        //sceneStarting = false;
        StartCoroutine("RestartCheckpointRoutine");
    }

    public IEnumerator EndSceneRoutine(int SceneNumber)
    {
        // Make sure the RawImage is enabled.
        FadeImg.enabled = true;
        do
        {
            // Start fading towards black.
            FadeToBlack();

            // If the screen is almost black...
            if (FadeImg.color.a >= 0.95f)
            {
                // ... reload the level
                SceneManager.LoadScene(SceneNumber);
                yield break;
            }
            else
            {
                yield return null;
            }
        } while (true);
    }

    public void EndScene(int SceneNumber)
    {
        sceneStarting = false;
        StartCoroutine("EndSceneRoutine", SceneNumber);
    }
}