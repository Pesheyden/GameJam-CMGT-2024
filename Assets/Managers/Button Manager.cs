using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public SoundPlayer sound;

    public void SetScene(string sceneName)
    {
        sound.StartPlayingProcess();
        GameManager.Instance.SetScene(sceneName);
    }
    public void QuitGame()
    {
        sound.StartPlayingProcess();
        GameManager.Instance.QuitGame();
    }

    public void RestartScene()
    {
        sound.StartPlayingProcess();
        GameManager.Instance.RestartScene();
    }

    public void ChangePauseUIStatus()
    {
        sound.StartPlayingProcess();
        GameManager.Instance.UnPauseGame();
    }
}
