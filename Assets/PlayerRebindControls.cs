using UnityEngine;
using UnityEngine.AI;

public class PlayerRebindControls : MonoBehaviour
{
    [SerializeField] private GameObject playerWASD;
    [SerializeField] private GameObject playerPathFinding;
    [SerializeField] private NavMeshAgent agent;

    private bool isSwapped;

    private void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.enabled = false;

        isSwapped = false;
        TogglePlayerControls();
    }

    public void SwapControls()
    {
        isSwapped = !isSwapped;
        TogglePlayerControls();
        HandleAgent();
    }

    private void TogglePlayerControls()
    {
        playerWASD.SetActive(!isSwapped);
        playerPathFinding.SetActive(isSwapped);
    }

    private void HandleAgent()
    {
        Debug.Log("agent enabled to : " + playerPathFinding.activeInHierarchy);
        agent.enabled = playerPathFinding.activeInHierarchy;
        if(agent.enabled){
            agent.ResetPath();
            agent.updateRotation = false;
            agent.updateUpAxis = false;

        }
            
    }
}
