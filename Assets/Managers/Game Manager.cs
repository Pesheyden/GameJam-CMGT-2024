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
        DontDestroyOnLoad(gameObject);
    }

    public void Death()
    {
        UIManager.Instance.ChangeDeathUIStatus(true);
        PlayerController.Instance.ChangePlayerStatus(false);
        PossessionTimer.Instance.PauseTimer();
    }

    public void PauseGame()
    {
        PlayerController.Instance.ChangePlayerStatus(false);
        PossessionTimer.Instance.PauseTimer();
        UIManager.Instance.ChangePauseUIStatus(true);
    }

    public void UnPauseGame()
    {
        PlayerController.Instance.ChangePlayerStatus(true);
        PossessionTimer.Instance.UnPauseTimer();
        UIManager.Instance.ChangePauseUIStatus(false);
    }

    public void GameWin()
    {
        UIManager.Instance.ChangeWinUIStatus(true);
    }
    public void SetScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
