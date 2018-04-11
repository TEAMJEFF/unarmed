using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayEnding : MonoBehaviour
{
    public ScreenFader screenFader;
    public VideoClip cutscene;
    public VideoPlayer videoPlayer;
    private VideoSource videoSource;
    private AudioSource cutsceneAudio;
    public RawImage blackScreen;
    private int skipThreshold = 0;
    private bool skipped = false;
    private bool allowSync = false;

    // Use this for initialization
    void Start()
    {
        screenFader.sceneStarting = false;
        cutsceneAudio = gameObject.AddComponent<AudioSource>();

        cutsceneAudio.playOnAwake = false;

        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, cutsceneAudio);

        videoPlayer.clip = cutscene;
        videoPlayer.Prepare();

        Destroy(blackScreen);
        cutsceneAudio.Play();
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
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
            Destroy(videoPlayer);
            Destroy(cutsceneAudio);
        }


        if (skipped)
        {
            screenFader.EndScene(0);
        }
    }
}
