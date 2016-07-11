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
    private Text hpText;
    private Text scoretText;
    private Text timerText;
    private GameObject overlay;
    private Scene currentScene;

    void Start () {
        nextLevelButton = GameObject.Find("NextLevelButton").GetComponent<Button>();
        restartLevelButton = GameObject.Find("RestartButton").GetComponent<Button>();
        returnToMenuButton = GameObject.Find("ReturnToMenuButton").GetComponent<Button>();
        sceneLoader = GameObject.Find("Main Camera").GetComponent<SceneLoader>();
        overlayTitle = GameObject.Find("OverlayTitleText").GetComponent<Text>();
        currentTimerText = GameObject.Find("CurrentTimerText").GetComponent<Text>();
        bestTimeText = GameObject.Find("RecordText").GetComponent<Text>();
        hpText = GameObject.Find("HpText").GetComponent<Text>();
        scoretText = GameObject.Find("ScoreText").GetComponent<Text>();
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        overlay = GameObject.Find("InGameOverlay");

        currentScene = SceneManager.GetActiveScene();
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

        UpdateTexts();
        GameVariables.currentState = GameVariables.StateType.PLAYING;
    }

    void OnGUI()
    {
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        timerText.text = GameVariables.currentTime.ToString("F2");
        hpText.text = "HP: " + GameVariables.currentHp.ToString();
        scoretText.text = "Pickups: " + GameVariables.currentPickups.ToString() + "/" + GameVariables.totalPickups;
        switch (GameVariables.currentState)
        {
            case GameVariables.StateType.PLAYING:
                HideOverlay();
                break;
            case GameVariables.StateType.PAUSE:
                ShowOverlay("Pause", false);
                break;
            case GameVariables.StateType.WIN:
                ShowOverlay("You win!", true);
                break;
            case GameVariables.StateType.LOST:
                ShowOverlay("You Lose!", false);
                break;
            default:
                HideOverlay();
                break;
        }
    }

    public void ShowOverlay(string title, bool allowNextLevel)
    {
        overlayTitle.text = title;
        Time.timeScale = 0;
        currentTimerText.text = "Current Time: " + GameVariables.currentTime.ToString("F2");
        bestTimeText.text = "Best Time: " + PlayerPrefs.GetFloat("BestTime" + currentScene.buildIndex).ToString("F2");
        overlay.SetActive(true);
        if (!allowNextLevel || ((sceneLoader.currentScene.buildIndex + 1) > (sceneLoader.totalScenes - 1)))
        {
            nextLevelButton.gameObject.SetActive(false);
        }
    }

    public void HideOverlay()
    {
        Time.timeScale = 1;
        nextLevelButton.gameObject.SetActive(true);
        overlay.SetActive(false);
    }
}
