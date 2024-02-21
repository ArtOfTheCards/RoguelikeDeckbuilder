using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{

    [Header("menu Screens")]
    [SerializeField] private GameObject loadingScreen;

    [SerializeField] private GameObject menuScreen;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    public void LoadlevelBtn(string levelToLoad){
        menuScreen.SetActive(false);
        loadingScreen.SetActive(true);

        // run async operatiion
        StartCoroutine(LoadLevelAsync(levelToLoad));

    }

    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress/ 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }

}
