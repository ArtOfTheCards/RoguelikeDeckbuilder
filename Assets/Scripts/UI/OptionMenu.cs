using System.Data.Common;
using UnityEngine;
using UnityEngine.AI;

public class OptionMenu : MonoBehaviour
{
     [SerializeField] private GameObject _optionScreen;
     [SerializeField] private GameObject playerWASD;
     [SerializeField] private GameObject playerPathfinding;

     private static bool isPaused;
     private static bool isRebindPressed;
     private void Awake()
     {
          _optionScreen.SetActive(false);
          playerWASD.SetActive(true);
          playerPathfinding.SetActive(false);

          isPaused = false;
          isRebindPressed = true;
     }
     public void OnPause()
     {
          isPaused = !isPaused;
          _optionScreen.SetActive(isPaused);
          Time.timeScale = isPaused ? 1.0f : 0;

     }

     public void OnRebindPlayerMovement()
     {
          isRebindPressed = !isRebindPressed;
          playerWASD.SetActive(isRebindPressed);
          playerPathfinding.SetActive(!isRebindPressed);
     }

}
