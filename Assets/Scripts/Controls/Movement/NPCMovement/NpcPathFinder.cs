

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NpcPathFinder : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject target = null;
    public List<GameObject> targets = new List<GameObject>();

    [SerializeField] private Transform startingPoint;
    // Trigger Checkables
    private bool isAggro;
    private bool isAttacking;

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

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

    }

    void Update()
    {
      
        if(targets.Capacity != 0){
            target = targets[0];
            SetDestination(target.transform.position);
        }else
            SetDestination(startingPoint.position);
    }

    private void Attack(GameObject target)
    {
        Debug.Log("Attacking Player");
    }

    #region TargetHandler
    public void AddTarget(GameObject target)
    {

        targets.Add(target);
        UpdateTargets();

    }


    public void RemoveTarget(GameObject target)
    {
        targets.Remove(target);
        UpdateTargets();
    }

    public void UpdateTargets()
    {
        targets.Sort((a, b) => Vector3.Distance(transform.position, a.transform.position)
                               .CompareTo(Vector3.Distance(transform.position, b.transform.position)));
    }

    #endregion




    public void SetAggroStatus(bool status)
    {
        isAggro = status;

    }

    public void SetAttackStatus(bool status)
    {
        isAttacking = status;
    }
}
