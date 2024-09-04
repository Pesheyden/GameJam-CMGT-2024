using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField] private GameObject _deathUI;
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private GameObject _winUI;

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
    

    public void ChangeDeathUIStatus(bool status)
    {
        _deathUI.SetActive(status);
    }

    public void ChangePauseUIStatus(bool status)
    {
        _pauseUI.SetActive(status);
    }

    public void ChangeWinUIStatus(bool status)
    {
        _winUI.SetActive(status);
    }
}
