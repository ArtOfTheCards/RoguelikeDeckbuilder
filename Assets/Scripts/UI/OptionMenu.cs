using TMPro;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
     [Header("Option Menu")]
     [SerializeField] private GameObject options;

     [Header("Player Components")]
     [SerializeField] private GameObject player;
     [SerializeField] private PlayerRebindControls rebindControls;
     [SerializeField] private GameObject cardUser;
     [SerializeField] private TextMeshProUGUI pauseButtonText;

     private bool isPaused;

     private void Awake()
     {
          FindPlayerComponents();
          SetPauseState(false);
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

     public void OnPause()
     {
          SetPauseState(!isPaused);
     }

     private void SetPauseState(bool paused)
     {
          isPaused = paused;
          options.SetActive(paused);
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
