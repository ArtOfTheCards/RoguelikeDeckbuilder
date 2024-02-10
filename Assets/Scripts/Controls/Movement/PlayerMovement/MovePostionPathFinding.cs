using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class MovePositionPathfinding : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Update()
    {        
        if (Input.GetMouseButton(0))
        {
            SetDestination(GetMouseWorldPosition());
        }
    }

    private void SetDestination(Vector3 destination)
    {
        _agent.destination = destination;
        PlayMovementAnimation(destination - transform.position);
    }

    private void PlayMovementAnimation(Vector3 movementDirection)
    {
        float angle = Vector3.SignedAngle(Vector3.forward, movementDirection.normalized, Vector3.up);
        SetAnimatorParameters(angle);
    }

    private void SetAnimatorParameters(float angle)
    {
        float x = 0f, y = 0f;

        if (angle >= -45 && angle < 45)
        {
            y = 1f; // Forward
        }
        else if (angle >= 45 && angle < 135)
        {
            x = 1f; // Right
        }
        else if (angle >= -135 && angle < -45)
        {
            x = -1f; // Left
        }
        else
        {
            y = -1f; // Backward
        }

        UpdateAnimatorParameters(x, y);
    }

    private void UpdateAnimatorParameters(float x, float y)
    {
        _animator.SetFloat("X", x);
        _animator.SetFloat("Y", y);
        _animator.SetBool("isWalking", Mathf.Abs(x) > 0.1f || Mathf.Abs(y) > 0.1f);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;

        return worldPosition;
    }

}
