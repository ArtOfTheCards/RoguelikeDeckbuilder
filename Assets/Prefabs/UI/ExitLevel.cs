using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitLevel : MonoBehaviour
{
    [SerializeField] private ExitSafeRoom door;
    [SerializeField] private AsyncLoader loader;
    [SerializeField] private GameObject leaveRoomImage; 

    [SerializeField] private GameObject settingScreen;


    private void Awake() {

        leaveRoomImage.SetActive(false);
        settingScreen = GameObject.Find("SettingScreen");
    }
    private void Update()
    {
        EnableLeaveRoomUI();
    }

    private void EnableLeaveRoomUI()
    {
        if(settingScreen)
            settingScreen.SetActive(false);
        leaveRoomImage.SetActive(door.IsPlayerAtDoor); // Enable the UI element
        Time.timeScale = door.IsPlayerAtDoor? 0f: 1.0f; 
    }

    public void OnYesButton()
    {
        loader.LoadlevelBtn("Sandbag-Level");
    }

    public void OnNoButton()
    {
        door.IsPlayerAtDoor = false;
    }
}
