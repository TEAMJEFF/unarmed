using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayCutscene : MonoBehaviour
{

    public VideoClip cutscene;
    public VideoPlayer videoPlayer;
    private VideoSource videoSource;
    private AudioSource cutsceneAudio;
    private int skipThreshold = 0;
    public Text skipText;
    private bool loadOnce = false;
    AsyncOperation asyncOperation;

    // Use this for initialization
    void Start()
    {
        cutsceneAudio = gameObject.AddComponent<AudioSource>();

        cutsceneAudio.playOnAwake = false;

        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, cutsceneAudio);

        videoPlayer.clip = cutscene;
        videoPlayer.Prepare();

        cutsceneAudio.Play();
        videoPlayer.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            skipThreshold++;
        }

        if (Input.anyKeyDown)
        {
            skipThreshold = 0;
        }

        if (!videoPlayer.isPlaying || skipThreshold > 120)
        {
            if (!loadOnce)
            {
                StartCoroutine(LoadScene());
                loadOnce = true;
            }
        }

    }


    IEnumerator LoadScene()
    {
        Destroy(videoPlayer);
        Destroy(cutsceneAudio);
        skipText.text = "Level Loading...";
        asyncOperation = SceneManager.LoadSceneAsync("levelOne");
        int count = 0;
        while (!asyncOperation.isDone)
        {
            count++;
            if (count > 10000)
            {
                Debug.Log("yeah its this");
                break;
            }
            yield return null;
        }
    }
}
