using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    public AudioClipSettings AudioClip;
    public int RepeatAmount;
    public StartType TypeStart;
    public float StartDelay;


    public bool IsMultipleClipsEnable = true;
    public int ClipsPlayAmount = -1;
    public float TimeBetweenClips = 0;
    public AudioClipSettings[] AudioClips;


    public bool IsRandomEnable = true;
    public RandomType TypeRandom;

    private AudioSource _audioSource;
    private int _currentClipIndex;
    private Queue<AudioClipSettings> _playQueue = new();
    private int _repeatsLeft;
    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();

        _repeatsLeft = RepeatAmount;

        switch (TypeStart)
        {
            case StartType.None:
                StartPlayingProcess();
                break;
            case StartType.Delay:
                Invoke(nameof(StartPlayingProcess), StartDelay);
                break;
            case StartType.Call:
                break;
            default:
                break;
        }             
    }
    public void StartPlayingProcess()
    {


        if (!IsMultipleClipsEnable)
        {
            _playQueue.Enqueue(AudioClip);
        }
        else if(!IsRandomEnable)
        {
            foreach (var clip in AudioClips)
            {
                _playQueue.Enqueue(clip);
            }
        }
        else
        {
            switch (TypeRandom)
            {
                case RandomType.Random:
                    if(ClipsPlayAmount == -1)
                    {
                        for (int i = 0; i < AudioClips.Length; i++)
                        {
                            _playQueue.Enqueue(AudioClips[Random.Range(0, AudioClips.Length)]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ClipsPlayAmount; i++)
                        {
                            _playQueue.Enqueue(AudioClips[Random.Range(0, AudioClips.Length)]);
                        }

                    }
                        
                    break;
                case RandomType.ExclusiveRandom:
                    List<AudioClipSettings> list = new List<AudioClipSettings>();
                    list.AddRange(AudioClips);
                    ClipsPlayAmount = Mathf.Clamp(ClipsPlayAmount, -1, list.Count);
                    if (ClipsPlayAmount == -1)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            var clip = list[Random.Range(0, list.Count)];
                            _playQueue.Enqueue(clip);
                            list.Remove(clip);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < ClipsPlayAmount; i++)
                        {
                            var clip = list[Random.Range(0, list.Count)];
                            _playQueue.Enqueue(clip);
                            list.Remove(clip);
                        }
                    }
                    
                    break;
                default:
                    Debug.LogError($"Random Type '{TypeRandom}' was not implemented");
                    break;
            }
        }


        PlayClip();
    }
    private void PlayClip()
    {
        var clip = _playQueue.Dequeue();
        _audioSource.volume = Random.Range(clip.MinVolume, clip.MaxVolume);
        _audioSource.pitch = Random.Range(clip.MinPitch, clip.MaxPitch);
        _audioSource.PlayOneShot(clip.AudioClip);
        if(_playQueue.Count > 0)
        {

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if(TimeBetweenClips == -1)
                Invoke(nameof(PlayClip), clip.AudioClip.length);
            else
                Invoke(nameof(PlayClip), TimeBetweenClips);
        }
        else if(_repeatsLeft != 0)
        {
            _repeatsLeft--;
            Invoke(nameof(StartPlayingProcess), clip.AudioClip.length);
        }
    }

    public enum StartType
    {
        None,
        Delay,
        Call
    }

    public enum RandomType
    {
        Random,
        ExclusiveRandom
    }
}

