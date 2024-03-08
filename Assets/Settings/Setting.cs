using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using System.Collections;
using System.ComponentModel.Design;

public class Setting : MonoBehaviour
{
    [Header("Tabs")]
    [SerializeField] private GameObject[] settingTabs;

    [Header("Settings")]
    [SerializeField] private SettingData defaultSetting;
    [SerializeField] private SettingData currentSetting;

    [Header("GamePlay UI")]
    [SerializeField] private TMP_Dropdown languageDropDown;
    [SerializeField] private Toggle freeLook;
    [SerializeField] private TextMeshProUGUI[] lookSensitivityTexts;
    [SerializeField] private Slider[] lookSensitivitySliders;

    [Header("Control UI")]
    [SerializeField] private Toggle pathFindingEnabled;

    [SerializeField] private TextMeshProUGUI[] rebindTexts;

    [SerializeField] private TMP_Dropdown upInputDropdown;
    [SerializeField] private TMP_Dropdown downInputDropdown;
    [SerializeField] private TMP_Dropdown leftInputDropdown;
    [SerializeField] private TMP_Dropdown rightInputDropdown;
    [SerializeField] private TMP_Dropdown interactInputDropdown;

    private Dictionary<KeyCode, int> keyCodeToDropdownValue; // Mapping KeyCode to TMP_Dropdown value

    private const float ROUNDING_FACTOR_X = 100f;
    private const float ROUNDING_FACTOR_Y = 10f;

    private bool languageDropDownActive = false;

    private void Awake()
    {
        InitializeKeyCodeToDropdownValue();
        UpdateSettingsUI();
        ActivateSettingsTab(0); // Assuming the first tab is active by default
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
        lookSensitivitySliders[0].value = currentSetting.freeLookSensitivity_X;
        lookSensitivitySliders[1].value = currentSetting.freeLookSensitivity_Y;
        pathFindingEnabled.isOn = currentSetting.pathFindingEnabled;
        UpdateLanguageDropdown();
        UpdateDropdownsFromKeyCode();

        UpdateUIInteractability();
    }

    private void UpdateUIInteractability()
    {
        foreach (var text in rebindTexts)
        {
            text.alpha = pathFindingEnabled.isOn ? 0.5f : 1f;
        }

        upInputDropdown.interactable = !pathFindingEnabled.isOn;
        downInputDropdown.interactable = !pathFindingEnabled.isOn;
        leftInputDropdown.interactable = !pathFindingEnabled.isOn;
        rightInputDropdown.interactable = !pathFindingEnabled.isOn;
        interactInputDropdown.interactable = !pathFindingEnabled.isOn;
    }

    private void UpdateDropdownsFromKeyCode()
    {
        UpdateDropdownFromKeyCode(upInputDropdown, currentSetting.up);
        UpdateDropdownFromKeyCode(downInputDropdown, currentSetting.down);
        UpdateDropdownFromKeyCode(leftInputDropdown, currentSetting.left);
        UpdateDropdownFromKeyCode(rightInputDropdown, currentSetting.right);
        UpdateDropdownFromKeyCode(interactInputDropdown, currentSetting.interact);
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

    public void ActivateSettingsTab(int tabIndex)
    {
        for (int i = 0; i < settingTabs.Length; i++)
        {
            settingTabs[i].SetActive(i == tabIndex);
        }
    }

    public void UpdateLanguageDropdown()
    {
        currentSetting.language = languageDropDown.value;
        StartCoroutine(SetLocale(currentSetting.language));
    }

    private IEnumerator SetLocale(int localeID)
    {
        yield return LocalizationSettings.InitializationOperation;

        if (LocalizationSettings.AvailableLocales.Locales.Count > localeID)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        }
    }


    public void UpdateFreeLook()
    {
        currentSetting.freeLook = freeLook.isOn;

        foreach (var text in lookSensitivityTexts)
        {
            text.alpha = freeLook.isOn ?1f : 0.5f;
        }
        foreach (var slider in lookSensitivitySliders)
        {
            slider.interactable = freeLook.isOn;
        }
    }

    public void UpdateLookSensitivityX()
    {
        currentSetting.freeLookSensitivity_X = Mathf.Round(lookSensitivitySliders[0].value * ROUNDING_FACTOR_X) / ROUNDING_FACTOR_X;
    }

    public void UpdateLookSensitivityY()
    {
        currentSetting.freeLookSensitivity_Y = Mathf.Round(lookSensitivitySliders[1].value * ROUNDING_FACTOR_Y) / ROUNDING_FACTOR_Y;
    }

    public void UpdatePathFindingEnabled()
    {
        currentSetting.pathFindingEnabled = pathFindingEnabled.isOn;
        UpdateUIInteractability();
    }

    public void UpdateUpInput()
    {
        UpdateSettingsFromDropdown(upInputDropdown, ref currentSetting.up);
    }

    public void UpdateDownInput()
    {
        UpdateSettingsFromDropdown(downInputDropdown, ref currentSetting.down);
    }

    public void UpdateLeftInput()
    {
        UpdateSettingsFromDropdown(leftInputDropdown, ref currentSetting.left);
    }

    public void UpdateRightInput()
    {
        UpdateSettingsFromDropdown(rightInputDropdown, ref currentSetting.right);
    }

    public void UpdateInteractInput()
    {
        UpdateSettingsFromDropdown(interactInputDropdown, ref currentSetting.interact);
    }

    public void ResetSettings()
    {
        currentSetting.SetTo(defaultSetting);
        UpdateSettingsUI();
    }
}
