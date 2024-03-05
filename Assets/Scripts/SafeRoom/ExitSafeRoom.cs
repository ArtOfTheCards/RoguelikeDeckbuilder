using UnityEngine;

public class ExitSafeRoom : MonoBehaviour
{
   [SerializeField] private bool isPlayerAtDoor;

    public bool IsPlayerAtDoor
    {
        get { return isPlayerAtDoor; }
        set { isPlayerAtDoor = value; }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is at the Door");
            isPlayerAtDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerAtDoor = false;
        }
    }
}
