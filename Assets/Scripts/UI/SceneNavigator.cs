using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneNavigator : MonoBehaviour
{

    // Change tagret graphic as that's the buttons background
    [Header("Options")]
    [SerializeField] private GameObject titleScreen;

    [SerializeField] private GameObject settingScreen;

    [SerializeField] private GameObject creditScreen;

    public Animator animator;



    private void Awake()
    {
        GoToMainMenu();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Sandbag-Level");
    }

    public void SaveSettings()
    {
        return;
    }

    public void ResetSettings()
    {

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