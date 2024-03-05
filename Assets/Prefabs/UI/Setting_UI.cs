using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Setting_UI : MonoBehaviour
{
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

    [SerializeField] private TextMeshProUGUI[] rebindText;
    [SerializeField] private TMP_Dropdown upInput;
    [SerializeField] private TMP_Dropdown downInput;
    [SerializeField] private TMP_Dropdown leftInput;
    [SerializeField] private TMP_Dropdown rightInput;
    [SerializeField] private TMP_Dropdown interactInput;

    private Dictionary<KeyCode, int> keyCodeToDropdownValue; // Mapping KeyCode to TMP_Dropdown value

    private void Awake()
    {
        InitializeKeyCodeToDropdownValue();
        UpdateSettingsUI();
        ActivateGamePlaySettings();
    }

    private void InitializeKeyCodeToDropdownValue()
    {
        keyCodeToDropdownValue = new Dictionary<KeyCode, int>();
        // Initialize mapping for regular keys
        for (int i = 0; i <= 25; i++)
        {
            keyCodeToDropdownValue.Add(KeyCode.A + i, i);
        }
        // Add mapping for special keys
        keyCodeToDropdownValue.Add(KeyCode.Tab, 26);
        keyCodeToDropdownValue.Add(KeyCode.CapsLock, 27);
        keyCodeToDropdownValue.Add(KeyCode.LeftShift, 28);
        keyCodeToDropdownValue.Add(KeyCode.LeftControl, 29);
        keyCodeToDropdownValue.Add(KeyCode.Space, 30);
        keyCodeToDropdownValue.Add(KeyCode.RightShift, 31);
        keyCodeToDropdownValue.Add(KeyCode.RightControl, 32);
        // Add mapping for mouse buttons
        for (int i = 0; i <= 6; i++)
        {
            keyCodeToDropdownValue.Add(KeyCode.Mouse0 + i, 33 + i);
        }
    }

    private void UpdateSettingsUI()
    {
        languageDropDown.value = currentSetting.language;
        freeLook.isOn = currentSetting.freeLook;
        lookSensitivity_X.value = currentSetting.freeLookSensitivity_X;
        lookSensitivity_Y.value = currentSetting.freeLookSensitivity_Y;
        pathFindingEnabled.isOn = currentSetting.pathFindingEnabled;

        // Update key code TMP_Dropdowns
        UpdateDropdownFromKeyCode(upInput, currentSetting.up);
        UpdateDropdownFromKeyCode(downInput, currentSetting.down);
        UpdateDropdownFromKeyCode(leftInput, currentSetting.left);
        UpdateDropdownFromKeyCode(rightInput, currentSetting.right);
        UpdateDropdownFromKeyCode(interactInput, currentSetting.interact);

        foreach (var item in rebindText)
        {
            item.alpha = pathFindingEnabled.isOn ? 0.5f : 1f;
        }
        upInput.interactable = pathFindingEnabled.isOn ? false : true;
        downInput.interactable = pathFindingEnabled.isOn ? false : true;
        leftInput.interactable = pathFindingEnabled.isOn ? false : true;
        rightInput.interactable = pathFindingEnabled.isOn ? false : true;
        interactInput.interactable = pathFindingEnabled.isOn ? false : true;
    }

    private void UpdateDropdownFromKeyCode(TMP_Dropdown dropdown, KeyCode keyCode)
    {
        int dropdownValue;
        if (keyCodeToDropdownValue.TryGetValue(keyCode, out dropdownValue))
        {
            dropdown.value = dropdownValue;
        }
    }

    private void UpdateSettingsFromDropdown(TMP_Dropdown dropdown, ref KeyCode keyCode)
    {
        int dropdownValue = dropdown.value;
        foreach (var kvp in keyCodeToDropdownValue)
        {
            if (kvp.Value == dropdownValue)
            {
                keyCode = kvp.Key;
                break;
            }
        }
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

    public void UpdateFreeLook()
    {
        currentSetting.freeLook = freeLook.isOn;

        lookSensitivity_X.interactable = freeLook.isOn ? false : true;
        lookSensitivity_Y.interactable = freeLook.isOn ? false : true;
        lookSensitivity_X_text.alpha = freeLook.isOn ? 0.5f : 1f;
        lookSensitivity_Y_text.alpha = freeLook.isOn ? 0.5f : 1f;
    }

    public void UpdateLookSensitivityX()
    {
        currentSetting.freeLookSensitivity_X = MathF.Round(lookSensitivity_X.value * 10f) / 10f;
    }

    public void UpdateLookSensitivityY()
    {
        currentSetting.freeLookSensitivity_Y = MathF.Round(lookSensitivity_Y.value * 10f)/10f;
    }

    public void UpdatePathFindingEnabled()
    {
        currentSetting.pathFindingEnabled = pathFindingEnabled.isOn;
        
        foreach (var item in rebindText)
        {
            item.alpha = pathFindingEnabled.isOn ? 0.5f : 1f;
        }
        upInput.interactable = pathFindingEnabled.isOn ? false : true;
        downInput.interactable = pathFindingEnabled.isOn ? false : true;
        leftInput.interactable = pathFindingEnabled.isOn ? false : true;
        rightInput.interactable = pathFindingEnabled.isOn ? false : true;
        interactInput.interactable = pathFindingEnabled.isOn ? false : true;
    }

    public void UpdateUpInput()
    {
        UpdateSettingsFromDropdown(upInput, ref currentSetting.up);
    }

    public void UpdateDownInput()
    {
        UpdateSettingsFromDropdown(downInput, ref currentSetting.down);
    }

    public void UpdateLeftInput()
    {
        UpdateSettingsFromDropdown(leftInput, ref currentSetting.left);
    }

    public void UpdateRightInput()
    {
        UpdateSettingsFromDropdown(rightInput, ref currentSetting.right);
    }

    public void UpdateInteractInput()
    {
        UpdateSettingsFromDropdown(interactInput, ref currentSetting.interact);
    }


    public void SaveSettings()
    {
        // Save settings to ScriptableObject or PlayerPrefs or any other storage method
        // Example: currentSetting.Save();
    }

    public void ResetSettings()
    {
        currentSetting = Instantiate<Setting>(defaultSetting);

        UpdateSettingsUI();
    }
}
