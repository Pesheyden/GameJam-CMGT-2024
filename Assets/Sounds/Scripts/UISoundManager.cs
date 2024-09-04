using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class UISoundManager : MonoBehaviour
{
    private static UISoundManager _instance;
    public static UISoundManager Instance => _instance;
    
    public UIAudioConfig[] UIAudioConfigs;
    private AudioSource _audioSource;

    private const string ENUM_NAME = "UIAudioConfingName";
    
    
    public void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("There can not be two UISoundManagers at the same time");
            Destroy(this);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this);
        
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void Play(UIAudioConfingName configName)
    {
        UIAudioConfig configToPlay = new();
        foreach (var config in UIAudioConfigs)
        {
            if (config.Name != configName.ToString()) continue;
            configToPlay = config;
            break;
        }

        if (configToPlay == new UIAudioConfig())
        {
            Debug.LogError($"UI Audio Config was not found or is empty with name {configName}");
            return;
        }
        
        var clip = configToPlay.AudioClips[Random.Range(0,configToPlay.AudioClips.Length)];
        _audioSource.volume = Random.Range(clip.MinVolume, clip.MaxVolume);
        _audioSource.pitch = Random.Range(clip.MinPitch, clip.MaxPitch);
        _audioSource.PlayOneShot(clip.AudioClip);
    }
#if UNITY_EDITOR
    public void RefreshEnums()
    {
        List<string> enumEntries = new(){"Nothing"};
        string filePathAndName = "Assets/Scripts/Enums/" + ENUM_NAME  + ".cs"; //The folder Scripts/Enums/ is expected to exist

        foreach (var config in UIAudioConfigs)
        {
            string text = config.Name;

            if (!char.IsUpper(text[0]))
            {
                Debug.LogError($"Invalid property name {config.Name}. First letter must ONLY BE UPPER");
                return;
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsLetterOrDigit(text[i]))
                {
                    Debug.LogError($"Invalid property name {config.Name}. Must ONLY contain letters or digits");
                    return; 
                }
            }
                        
            enumEntries.Add(text);
        }
                
        if(enumEntries.Count == 1)
            return;
                
        using ( var streamWriter = new StreamWriter( filePathAndName ) )
        {
            streamWriter.WriteLine( "public enum " + ENUM_NAME );
            streamWriter.WriteLine( "{" );
            foreach (var t in enumEntries)
            {
                streamWriter.WriteLine( "	" + t + "," );
            }
            streamWriter.WriteLine( "}" );
        }
        AssetDatabase.Refresh();
    }
#endif
}

[Serializable]
public class UIAudioConfig
{
    public string Name;
    public AudioClipSettings[] AudioClips;
}

[Serializable]
public class AudioClipSettings
{
    public AudioClip AudioClip;
    [Range(0f, 1f)]
    public float MinVolume = 1;
    [Range(0f, 1f)]
    public float MaxVolume = 1;
    [Range(0f, 1f)]
    public float MinPitch = 1;
    [Range(0f, 1f)]
    public float MaxPitch = 1;
}
