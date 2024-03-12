using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LanguageOptionsInitializer : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private SettingData currentSetting;

    [SerializeField] private Button en_btn;

    [SerializeField] private Button es_btn;

    [SerializeField] private Button zh_btn;

    private bool active = false;
    
    void Awake  ()
   {
        mainMenu.SetActive(false);
        this.gameObject.SetActive(true);
   }



    public void UpdateLanguage(int _localID)
    {
        currentSetting.language = _localID;
        mainMenu.SetActive(true);
        if(active)
            return;
        StartCoroutine(SetLocale(currentSetting.language));
    }

    private IEnumerator SetLocale(int localeID)
    {
        yield return LocalizationSettings.InitializationOperation;

        if (LocalizationSettings.AvailableLocales.Locales.Count > localeID)
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        this.gameObject.SetActive(false);
    }
}
