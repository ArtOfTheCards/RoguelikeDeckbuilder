using UnityEngine;

public class OptionMenu : MonoBehaviour
{
     [SerializeField] private GameObject optionScreen;
     [SerializeField] private PlayerRebindControls rebindControls;

     private bool isPaused;

     private void Awake()
     {
          SetPauseState(false);
     }

     public void OnPause()
     {
          SetPauseState(!isPaused);
     }

     private void SetPauseState(bool paused)
     {
          isPaused = paused;
          optionScreen.SetActive(paused);
          Time.timeScale = paused ? 0f : 1f;
     }

     public void OnRebindPlayerMovement()
     {
          rebindControls.SwapControls();
     }
}
