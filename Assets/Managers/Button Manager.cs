using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{


    public void SetScene(string sceneName)
    {
        GameManager.Instance.SetScene(sceneName);
    }
    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }

    public void RestartScene()
    {
        GameManager.Instance.RestartScene();
    }

    public void ChangePauseUIStatus(bool status)
    {
        UIManager.Instance.ChangePauseUIStatus(status);
    }
}
