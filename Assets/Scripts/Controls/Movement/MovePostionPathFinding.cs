using UnityEngine;
using UnityEngine.AI;

public class MovePostionPathFinding : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    private void SetDestination(Vector3 destination)
    {
        _agent.destination = destination;

    }

    private static Vector3 GetMouseWorldPostion()
    {
        Vector3 screenPostion = Input.mousePosition;
        Vector3 worldPostion = Camera.main.ScreenToWorldPoint(screenPostion);
        worldPostion.z = 0;

        return worldPostion;
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Update()
    {

        Vector3 mousePos = GetMouseWorldPostion();
        Debug.Log(mousePos);
        if (Input.GetMouseButton(0))
        {
            SetDestination(mousePos);

        }
    }

}
