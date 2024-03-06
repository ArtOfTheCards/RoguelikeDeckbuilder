using UnityEngine;
using UnityEngine.AI;

public class PlayerRebindControls : MonoBehaviour
{
    [SerializeField] private GameObject playerWASD;
    [SerializeField] private GameObject playerPathFinding;
    [SerializeField] private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void SwapControls(bool enablePathFinding)
    {
        playerWASD.SetActive(!enablePathFinding);
        playerPathFinding.SetActive(enablePathFinding);
        

        if (enablePathFinding)
        {
            agent.ResetPath();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        agent.enabled = enablePathFinding;

    }
}
