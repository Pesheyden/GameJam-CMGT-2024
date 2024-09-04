using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionTimer : MonoBehaviour
{
    private static PossessionTimer _instance;
    public static PossessionTimer Instance => _instance;

    private float _currentTime;
    private bool _isPaused;
    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError($"2 {name} could not exist in same time");
            Destroy(this);
        }
        _instance = this;

        _isPaused = false;
    }

    public void StartTimer(float time)
    {
        StartCoroutine(Timer(time));
        _currentTime = time;
    }

    private IEnumerator Timer(float time)
    {
        while (_currentTime > 0)
        {
            if(_isPaused)
                yield break;
            
            _currentTime -= Time.deltaTime;
            UIManager.Instance.UpdateTimerFillingAmount(_currentTime / time);
            yield return null;
        }
        
        PlayerController.Instance.StartShooting();
    }

    public void PauseTimer()
    {
        _isPaused = true;
    }

    public void UnPauseTimer()
    {
        _isPaused = false;
        StartTimer(_currentTime);
    }
}
