using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OverlayController : MonoBehaviour {

    private Button nextLevelButton;
    private Button restartLevelButton;
    private Button returnToMenuButton;
    private SceneLoader sceneLoader;
    private Text overlayTitle;
    private Text currentTimerText;
    private Text bestTimeText;

    void Awake () {
        nextLevelButton = GameObject.Find("NextLevelButton").GetComponent<Button>();
        restartLevelButton = GameObject.Find("RestartButton").GetComponent<Button>();
        returnToMenuButton = GameObject.Find("ReturnToMenuButton").GetComponent<Button>();
        sceneLoader = GameObject.Find("Main Camera").GetComponent<SceneLoader>();
        overlayTitle = GameObject.Find("OverlayTitleText").GetComponent<Text>();
        currentTimerText = GameObject.Find("CurrentTimerText").GetComponent<Text>();
        bestTimeText = GameObject.Find("RecordText").GetComponent<Text>();

        Scene currentScene = SceneManager.GetActiveScene();
        float currentRecord = PlayerPrefs.GetFloat("BestTime" + currentScene.buildIndex);
        if (currentRecord > 0)
            bestTimeText.text = "Best Time: " + currentRecord.ToString("F2");

        nextLevelButton.onClick.AddListener(delegate () {
            sceneLoader.LoadNextLevel();
        });

        restartLevelButton.onClick.AddListener(delegate () {
            sceneLoader.RestartLevel();
        });

        returnToMenuButton.onClick.AddListener(delegate () {
            sceneLoader.ReturnToMenu();
        });
    }

    public void ShowOverlay(string title, bool allowNextLevel, float timeEllpased)
    {
        overlayTitle.text = title;
        Time.timeScale = 0;
        currentTimerText.text = "Current Time: " + timeEllpased.ToString("F2");
        gameObject.SetActive(true);
        if (!allowNextLevel || ((sceneLoader.currentScene.buildIndex + 1) > (sceneLoader.totalScenes - 1)))
        {
            nextLevelButton.gameObject.SetActive(false);
        }
    }

    public void HideOverlay()
    {
        Time.timeScale = 1;
        nextLevelButton.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
