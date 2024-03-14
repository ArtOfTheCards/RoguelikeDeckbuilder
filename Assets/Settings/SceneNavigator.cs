using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneNavigator : MonoBehaviour
{

    [SerializeField] private SettingData currentSetting;

    // Change tagret graphic as that's the buttons background
    [Header("Options")]
    [SerializeField] private GameObject titleScreen;

    [SerializeField] private GameObject settingScreen;

    [SerializeField] private GameObject creditScreen;

    [SerializeField] private AsyncLoader loader;



    private void Awake()
    {
        loader = GameObject.FindObjectOfType<AsyncLoader>();
        GoToMainMenu();
    }

    public void StartGame()
    {
       loader.LoadlevelBtn("SafeRoom");
    }


    public void GoToMainMenu()
    {
        titleScreen.SetActive(true);
        settingScreen.SetActive(false);
        creditScreen.SetActive(false);

    }

    public void GoToSettings()
    {
        titleScreen.SetActive(false);
        settingScreen.SetActive(true);
        creditScreen.SetActive(false);
    }

    public void GoToCredits()
    {
        titleScreen.SetActive(false);
        settingScreen.SetActive(false);
        creditScreen.SetActive(true);
    }



    public void ExitGame()
    {
        Debug.Log("Quiting the Game.");
        Application.Quit();
    }
}