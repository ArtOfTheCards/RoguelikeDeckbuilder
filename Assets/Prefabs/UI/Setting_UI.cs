using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_UI : MonoBehaviour
{
    [Header("Tabs")]
    [SerializeField] private GameObject gamePlaySetting;

    [SerializeField] private GameObject controlsSetting;

    [SerializeField] private GameObject audioSetting;

    private void Awake() {
        
        ActivateGamePlaySettings();
    }

    public void ActivateGamePlaySettings()
    {
        gamePlaySetting.SetActive(true);
        controlsSetting.SetActive(false);
        audioSetting.SetActive(false);
    }


    public void ActivateControlsSettings()
    {
        gamePlaySetting.SetActive(false);
        controlsSetting.SetActive(true);
        audioSetting.SetActive(false);
    }


    public void ActivateAudioSettings()
    {
        gamePlaySetting.SetActive(false);
        controlsSetting.SetActive(false);
        audioSetting.SetActive(true);
    }
}
