using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Button script used to restart from checkpoint

public class RestartCheckPointButton : PausingScript
{

    public Button button;

    private ScreenFader fadeScr;

    // Use this for initialization
    void Start()
    {
        Button btn = button.GetComponent<Button>();
        btn.onClick.AddListener(ifClicked);
        fadeScr = FindObjectOfType<ScreenFader>();
    }

    void ifClicked()
    {
        fadeScr.RestartCheckpoint();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        unPause();
    }
}
