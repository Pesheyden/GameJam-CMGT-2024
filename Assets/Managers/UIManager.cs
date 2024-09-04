using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField] private GameObject _deathUI;

    [SerializeField] private Image _possessionTimerFillingImage;

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

    public void UpdateTimerFillingAmount(float amount)
    {
        _possessionTimerFillingImage.fillAmount = amount;
    }
}
