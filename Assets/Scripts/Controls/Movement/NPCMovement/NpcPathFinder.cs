using UnityEngine;
using UnityEngine.AI;
public class NpcPathFinder : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _target;

    private bool isAggro;
    private bool isAttacking;

    public Transform startingPos;
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

    }

    private void SetDestination(Vector3 destination)
    {
        _agent.destination = destination;
    }

    public void SetSpeed(float speed)
    {
        _agent.speed = speed;
    }

    public float GetSpeed()
    {
        return _agent.speed;
    }

    [System.Obsolete]
    private void StopPath()
    {
        _agent.Stop();
    }

    private void Start()
    {


    }

    void Update()
    {
        SetDestination(_target.transform.position);
    }

    public void SetAggroStatus(bool status)
    {
        isAggro = status;

    }

    public void SetAttackStatus(bool status)
    {
        isAttacking = status;
    }
}
