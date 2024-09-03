using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError($"2 {name} could not exist in same time");
            Destroy(this);
        }
        DontDestroyOnLoad(this);
        _instance = this;
    }

    public void Death()
    {
        UIManager.Instance.ChangeDeathUIStatus(true);
    }

    public void SceneMainMenu()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
    }
}
