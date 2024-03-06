using Cinemachine;
using TMPro;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
     [Header("Settings")]
     [SerializeField] private SettingData currentSettings;

     [Header("UI Components")]
     [SerializeField] private GameObject settingScreen;
     [SerializeField] private TextMeshProUGUI pauseButtonText;

     [Header("Camera Components")]
     [SerializeField] private CinemachineVirtualCamera followPlayerCamera2d;
     [SerializeField] private CinemachineFreeLook freeLookCamera;

     [Header("Player Components")]
     [SerializeField] private GameObject player;
     [SerializeField] private PlayerRebindControls rebindControls;
     [SerializeField] private GameObject cardUser;

     private bool isPaused;

     private void Awake()
     {
          FindPlayerComponents();
          SetPauseState(false);
     }

     private void Update()
     {
          UpdatePlayerCamera();
     }

     private void FindPlayerComponents()
     {
          player = GameObject.FindGameObjectWithTag("Player");

          if (player != null)
          {
               player.SetActive(true);
               rebindControls = player.GetComponentInChildren<PlayerRebindControls>();
               if (rebindControls != null)
                    rebindControls.enabled = true;
               cardUser.SetActive(true);
          }
          else
          {
               player = null;
               rebindControls = null;
               cardUser = null;
          }
     }

     private void UpdatePlayerCamera()
     {
          followPlayerCamera2d.enabled = !currentSettings.freeLook;
          freeLookCamera.enabled = currentSettings.freeLook;

          if (freeLookCamera.enabled)
          {
               freeLookCamera.m_XAxis.m_MaxSpeed = currentSettings.freeLookSensitivity_X;
               freeLookCamera.m_YAxis.m_MaxSpeed = currentSettings.freeLookSensitivity_Y;
          }
     }

     public void OnPause()
     {
          SetPauseState(!isPaused);
     }

     private void SetPauseState(bool paused)
     {
          isPaused = paused;
          settingScreen.SetActive(paused);
          if (cardUser != null)
               cardUser.SetActive(!paused);
          Time.timeScale = paused ? 0f : 1f;
          Debug.Log(Time.timeScale);
     }

     public void OnRebindPlayerMovement()
     {
          if (rebindControls != null)
               rebindControls.SwapControls();
     }
}
