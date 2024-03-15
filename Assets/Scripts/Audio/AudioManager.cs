using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Dynamic;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    public SettingData settings;

    private Bus MasterBus;
    private Bus BackgroundBus;
    private Bus SFXBus;

    private EventInstance musicEventInstance;


    public static AudioManager instance { get; private set; }

    public Slider Master;
    public Slider Background;
    public Slider SFX;
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

        Master.onValueChanged.AddListener(setMasterVolume);
        Background.onValueChanged.AddListener(setBackgroundVolume);
        SFX.onValueChanged.AddListener(setSFXVolume);

        Master.value = settings.masterVolume;
        SFX.value = settings.sfxVolume;
        Background.value = settings.ambientVolume;
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

    private void setMasterVolume(float v) {
        MasterBus.getVolume(out float initialMasterVol);
        float deltaVol = v-initialMasterVol;

        Background.value += deltaVol;
        SFX.value += deltaVol;

        MasterBus.setVolume(v);
        settings.masterVolume = v;
    }

    private void setBackgroundVolume(float v) {
        BackgroundBus.setVolume(v);
        settings.ambientVolume = v;
    }

    private void setSFXVolume(float v) {
        SFXBus.setVolume(v);
        settings.sfxVolume = v;
    }
}
