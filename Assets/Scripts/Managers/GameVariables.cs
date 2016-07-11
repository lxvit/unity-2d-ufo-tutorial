using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class GameVariables {
    public static int totalPickups;
    public static int currentPickups;
    public static float currentHp;
    public static float currentTime;
    public static StateType currentState;
    public static Scene currentScene;

    public enum StateType
    {
        WIN,
        LOST,
        PAUSE,
        PLAYING
    };
}
