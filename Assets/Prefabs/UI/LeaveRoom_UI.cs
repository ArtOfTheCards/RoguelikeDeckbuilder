using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveRoom_UI : MonoBehaviour
{
    [SerializeField] private ExitSafeRoom door;
    [SerializeField] private AsyncLoader loader;
    [SerializeField] private GameObject leaveRoomImage; // Reference to the UI element to be enabled/disabled


    private void Start() {
        leaveRoomImage.SetActive(false);
    }
    private void Update()
    {
        EnableLeaveRoomUI();
    }

    private void EnableLeaveRoomUI()
    {
        leaveRoomImage.SetActive(door.IsPlayerAtDoor); // Enable the UI element
        Time.timeScale = door.IsPlayerAtDoor? 0f: 1.0f; 
    }

    public void OnYesButton(string levelToLoad)
    {
        loader.LoadlevelBtn(levelToLoad);
    }

    public void OnNoButton()
    {
        door.IsPlayerAtDoor = false;
    }
}
