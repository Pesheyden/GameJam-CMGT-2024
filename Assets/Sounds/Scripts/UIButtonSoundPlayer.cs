using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonSoundPlayer : MonoBehaviour
{
    [SerializeField] private UIAudioConfingName _name;
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Play);
    }

    private void Play()
    {
        UISoundManager.Instance.Play(_name);
    }
}
