using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;
    [Range(0, 1)]
    public float backgroundVolume = 1;
    [Range(0, 1)]
    public float SFXVolume = 1;

    private Bus MasterBus;
    private Bus BackgroundBus;
    private Bus SFXBus;

    private EventInstance musicEventInstance;


    public static AudioManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one AudioManager in the scene");
        }
        instance = this;
        
        MasterBus = RuntimeManager.GetBus("bus:/");
        BackgroundBus = RuntimeManager.GetBus("bus:/BackgroundAudio");
        SFXBus = RuntimeManager.GetBus("bus:/SFXAudio");
    }
    private void Start()
    {
        InitializeMusic(FMODEvents.instance.BattleTheme);
    }

    private void Update()
    {
        MasterBus.setVolume(masterVolume);
        BackgroundBus.setVolume(backgroundVolume);
        SFXBus.setVolume(SFXVolume);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPosition)
    {
        RuntimeManager.PlayOneShot(sound, worldPosition);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    private void InitializeMusic(EventReference musicEventReference) 
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }
}
