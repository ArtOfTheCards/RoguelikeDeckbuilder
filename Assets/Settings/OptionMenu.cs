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
     [SerializeField] private CinemachineVirtualCamera followPlayerCamera2d = null;
     [SerializeField] private CinemachineFreeLook freeLookCamera = null;

     [Header("Player Components")]
     [SerializeField] private GameObject player;
     [SerializeField] private PlayerRebindControls rebindControls;
     [SerializeField] private GameObject cardUser;

     [SerializeField] private PlayerMovementKeys playerMovementKeys;

     private bool isPaused;

     private void Awake()
     {
          FindPlayerComponents();
          SetPauseState(false);
     }

     private void Update()
     {
          if (followPlayerCamera2d != null || freeLookCamera != null)
               UpdatePlayerCamera();

          UpdateControls();
     }

     private void FindPlayerComponents()
     {
          player = GameObject.FindGameObjectWithTag("Player");

          if (player != null)
          {
               player.SetActive(true);
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
          if (followPlayerCamera2d != null)
               followPlayerCamera2d.enabled = !currentSettings.freeLook;

          if (freeLookCamera != null)
          {
               freeLookCamera.enabled = currentSettings.freeLook;

               if (freeLookCamera.enabled)
               {
                    freeLookCamera.m_XAxis.m_MaxSpeed = currentSettings.freeLookSensitivity_X;
                    freeLookCamera.m_YAxis.m_MaxSpeed = currentSettings.freeLookSensitivity_Y;
               }
          }
     }

     private void UpdateControls()
     {
          rebindControls.SwapControls(currentSettings.pathFindingEnabled);
          if(currentSettings.pathFindingEnabled)
          {
              playerMovementKeys.Rebindkeys(currentSettings);

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
}
