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
    public RawImage bg;
    private int skipThreshold = 0;
    public Text skipText;
    private bool skipped = false;
    private bool fadeOut = false;
    private bool allowSync = false;
    private float fadeRate = 0.5f;
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

        asyncOperation = SceneManager.LoadSceneAsync("levelOne");
        asyncOperation.allowSceneActivation = false;

        cutsceneAudio.Play();
        videoPlayer.Play();


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(asyncOperation.progress.ToString());

        if (Input.anyKeyDown)
        {
            skipThreshold = 0;
        }

        if (Input.anyKey)
        {
            skipThreshold++;
        }

        if (!skipped && (!videoPlayer.isPlaying || skipThreshold > 120))
        {
            skipped = true;
            fadeOut = true;
            Destroy(videoPlayer);
            Destroy(cutsceneAudio);
        }


        if (fadeOut)
        {
            Color newCol = bg.color;
            if (newCol.r > 0.0001f)
            {
                newCol.r = Mathf.Lerp(bg.color.r, 0, fadeRate * Time.deltaTime);
                newCol.g = Mathf.Lerp(bg.color.g, 0, fadeRate * Time.deltaTime);
                newCol.b = Mathf.Lerp(bg.color.b, 0, fadeRate * Time.deltaTime);
                bg.color = newCol;
            }
        }

        if (!allowSync && skipped && asyncOperation.progress >= 0.9f)
        {
            allowSync = true;
            asyncOperation.allowSceneActivation = true;
        }
    }
}
