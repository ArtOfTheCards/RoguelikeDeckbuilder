using UnityEngine;
using UnityEngine.AI;

public class MovePositionPathfinding : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator animator;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            SetDestination(GetMouseWorldPosition());

        UpdateAnimationParameters();
    }

    private void SetDestination(Vector3 destination)
    {
        agent.destination = destination;
    }

    private void UpdateAnimationParameters()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Agent has reached its destination, reset animation parameters
            animator.SetFloat("X", 0);
            animator.SetFloat("Y", 0);
        }
        else
        {
            // Agent is still moving, update animation parameters based on movement direction
            Vector3 movementDirection = agent.desiredVelocity.normalized;
            float angle = Vector3.SignedAngle(Vector3.forward, movementDirection, Vector3.up);
            float x = 0f, y = 0f;

            if (angle >= -45 && angle < 45)
                y = 1f; // Forward
            else if (angle >= 45 && angle < 135)
                x = 1f; // Right
            else if (angle >= -135 && angle < -45)
                x = -1f; // Left
            else
                y = -1f; // Backward

            animator.SetFloat("X", x);
            animator.SetFloat("Y", y);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
        return worldPosition;
    }
}
