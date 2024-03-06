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
        agent = GetComponentInParent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.enabled = true;

        SwapControls(true);
    }

    public void SwapControls(bool enablePathFinding)
    {
        isSwapped = enablePathFinding;
        TogglePlayerControls(isSwapped);
        HandleAgent();
    }

    private void TogglePlayerControls(bool enablePathFinding)
    {
        playerWASD.SetActive(!enablePathFinding);
        playerPathFinding.SetActive(enablePathFinding);
    }

    private void HandleAgent()
    {
        agent.enabled = playerPathFinding.activeInHierarchy;
        if (agent.enabled)
        {
            agent.ResetPath();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
    }
}
