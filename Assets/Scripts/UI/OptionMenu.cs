using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
     [SerializeField] private GameObject options;
     [SerializeField] private PlayerRebindControls rebindControls;

     [SerializeField] private TextMeshPro pauseButtonText;
     private bool isPaused;

     private void Awake()
     {
          SetPauseState(false);
          pauseButtonText.color = Color.clear;
     }

     public void OnPause()
     {
          SetPauseState(!isPaused);
     }

     private void SetPauseState(bool paused)
     {
          isPaused = paused;
          options.SetActive(paused);
          Time.timeScale = paused ? 0f : 1f;
     }

     public void OnRebindPlayerMovement()
     {
          rebindControls.SwapControls();
     }
}
