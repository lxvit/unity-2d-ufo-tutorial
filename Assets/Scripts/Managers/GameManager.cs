using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private bool highScoreSaved;

    // Use this for initialization
    void Awake () {
        GameVariables.totalPickups = GameObject.FindGameObjectsWithTag("PickUp").Length;
        GameVariables.currentPickups = 0;
        GameVariables.currentTime = 0f;
        GameVariables.currentScene = SceneManager.GetActiveScene();
        Time.timeScale = 1;
        highScoreSaved = false;
        GameVariables.currentState = GameVariables.StateType.PLAYING;
    }
	
	// Update is called once per frame
	void Update () {
        GameVariables.currentTime = GameVariables.currentTime + Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameVariables.currentState == GameVariables.StateType.PAUSE)
            {
                GameVariables.currentState = GameVariables.StateType.PLAYING;
            } else
            {
                GameVariables.currentState = GameVariables.StateType.PAUSE;
            }
        }

        if (GameVariables.currentState == GameVariables.StateType.WIN)
        {
            SaveHighScore();
        }
    }

    public void SaveHighScore()
    {
        float currentRecord = PlayerPrefs.GetFloat("BestTime" + GameVariables.currentScene.buildIndex);
        if (GameVariables.currentTime < currentRecord || currentRecord == 0)
        {
            PlayerPrefs.SetFloat("BestTime" + GameVariables.currentScene.buildIndex, GameVariables.currentTime);
            currentRecord = GameVariables.currentTime;
        }
        highScoreSaved = true;
    }
}
