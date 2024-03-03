using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Setting_UI : MonoBehaviour
{
    [SerializeField] private OptionMenu optionMenuption;

    [Header("Tabs")]
    [SerializeField] private GameObject gamePlaySetting;
    [SerializeField] private GameObject controlsSetting;
    [SerializeField] private GameObject audioSetting;

    [SerializeField] private Setting defaultSetting;
    [SerializeField] private Setting currentSetting;

    [Header("GamePlay UI")]
    [SerializeField] private TMP_Dropdown languageDropDown;
    [SerializeField] private Toggle freeLook;
    [SerializeField] private TextMeshProUGUI lookSensitivity_X_text;
    [SerializeField] private TextMeshProUGUI lookSensitivity_Y_text;
    [SerializeField] private Slider lookSensitivity_X;
    [SerializeField] private Slider lookSensitivity_Y;

    [Header("Control UI")]
    [SerializeField] private Toggle pathFindingEnabled;

    // [Header("Audio UI")]
    // [SerializeField] private Slider volume;

    private void Awake()
    {
        UpdateSettings();
        ActivateGamePlaySettings();
    }

    private void UpdateSettings()
    {
        languageDropDown.value = currentSetting.language;
        lookSensitivity_X.value = currentSetting.camera_X;
        lookSensitivity_Y.value = currentSetting.camera_Y;
        pathFindingEnabled.isOn = currentSetting.pathFindingEnabled;
        // volume.value = currentSetting.volume;
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

    public void UpdateLanguageDropdown()
    {
        currentSetting.language = languageDropDown.value;
    }

 

    public void UpdateFreeLook(bool isOn)
    {
        currentSetting.freeLook = isOn;
        if (isOn)
        {
            lookSensitivity_X.interactable = false;
            lookSensitivity_Y.interactable = false;
            lookSensitivity_X_text.alpha = 0.5f;
            lookSensitivity_Y_text.alpha = 0.5f;
        }
        else
        {
            lookSensitivity_X.interactable = true;
            lookSensitivity_Y.interactable = true;
            lookSensitivity_X_text.alpha = 1f;
            lookSensitivity_Y_text.alpha = 1f;
        }
    }

    public void UpdateLookSensitivityX(float value)
    {
        currentSetting.camera_X = value;
    }

    public void UpdateLookSensitivityY(float value)
    {
        currentSetting.camera_Y = value;
    }



    public void UpdatePathFindingEnabled(bool isOn)
    {
        currentSetting.pathFindingEnabled = isOn;
    }

    // public void UpdateVolume(float value)
    // {
    //     currentSetting.volume = value;
    // }

    // Save settings method
    public void SaveSettings()
    {
        // Save settings to ScriptableObject or PlayerPrefs or any other storage method
        // Example: currentSetting.Save();
    }

    public void ResetSettings()
    {
        currentSetting = defaultSetting;
        UpdateSettings();
    }
}
