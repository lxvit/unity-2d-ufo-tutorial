using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public SceneLoader sceneLoader;
    private OverlayController overlayController;
    private Scene currentScene;

    private Text bestTimeText;

    private Text timerText;
    private float timeEllpased;

    private bool pauseToggle;
    public bool isPaused;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1;
        sceneLoader = GameObject.Find("Main Camera").GetComponent<SceneLoader>();
        overlayController = GameObject.Find("InGameOverlay").GetComponent<OverlayController>();
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        timerText.text = 0.00f.ToString();
        currentScene = SceneManager.GetActiveScene();
        bestTimeText = GameObject.Find("RecordText").GetComponent<Text>();
        HideOverlay();
    }
	
	// Update is called once per frame
	void Update () {
        timeEllpased = timeEllpased + Time.deltaTime;
        timerText.text = timeEllpased.ToString("F2");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseToggle)
            {
                isPaused = false;
                HideOverlay();
            } else
            {
                isPaused = true;
                ShowOverlay("Pause", false);
            }

            pauseToggle = !pauseToggle;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            sceneLoader.RestartLevel();
        }
    }

    public void ShowOverlay(string title, bool allowNextLevel)
    {
        overlayController.ShowOverlay(title, allowNextLevel, timeEllpased);
    }

    public void HideOverlay()
    {
        overlayController.HideOverlay();
    }

    public void SaveHighScore()
    {
        float currentRecord = PlayerPrefs.GetFloat("BestTime" + currentScene.buildIndex);
        if (timeEllpased < currentRecord || currentRecord == 0)
        {
            PlayerPrefs.SetFloat("BestTime" + currentScene.buildIndex, timeEllpased);
            currentRecord = timeEllpased;
        }
        bestTimeText.text = "Best Time: " + currentRecord.ToString("F2");
    }
}
