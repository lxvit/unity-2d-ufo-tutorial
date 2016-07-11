using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {

    private GameObject loadingImage;
    public Scene currentScene;
    public int totalScenes;

    void Awake ()
    {
        loadingImage = GameObject.Find("LoadingImage");
        currentScene = SceneManager.GetActiveScene();
        totalScenes = SceneManager.sceneCountInBuildSettings;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void LoadLevel(string sceneName)
    {
        loadingImage.SetActive(true);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevel(int sceneIndex)
    {
        loadingImage.SetActive(true);
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadNextLevel()
    {
        int nextSceneIndex = currentScene.buildIndex + 1;
        nextSceneIndex = nextSceneIndex > (totalScenes - 1) ? 0 : nextSceneIndex;
        LoadLevel(nextSceneIndex);
    }

    public void ReturnToMenu()
    {
        LoadLevel(0);
    }

    public void RestartLevel()
    {
        LoadLevel(currentScene.name);
    }
}
