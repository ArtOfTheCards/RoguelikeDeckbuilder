using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
          player = GameObject.FindGameObjectWithTag("Player");
          player.SetActive(true);
          rebindControls = player.GetComponentInChildren<PlayerRebindControls>();
          rebindControls.enabled = true;
          cardUser = GameObject.Find("CardUser");
          cardUser.SetActive(true);

          SetPauseState(false);
     }

     public void OnPause()
     {
          SetPauseState(!isPaused);
     }

     private void SetPauseState(bool paused)
     {
          isPaused = paused;
          options.SetActive(paused);
          cardUser.SetActive(!paused);
          Time.timeScale = paused ? 0f : 1f;

     }

     public void OnRebindPlayerMovement()
     {
          rebindControls.SwapControls();
     }
}
