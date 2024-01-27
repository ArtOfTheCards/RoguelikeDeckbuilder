using UnityEngine;
using UnityEngine.AI;
public class NpcPathFinder : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private CircleCollider2D _aggro;
    [SerializeField] private GameObject _target;

    



    public Transform startingPos;
    private void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
    }

    private void SetDestination(Vector3 destination){
        _agent.destination = destination;
    }

    private void SetSpeed(float speed){
        _agent.speed = speed;
    }

    [System.Obsolete]
    private void StopPath(){
        _agent.Stop();
    }

    private void Start() 
    {
        this.gameObject.transform.position = startingPos.position;
        
    }

    void Update()
    {

    }
}
