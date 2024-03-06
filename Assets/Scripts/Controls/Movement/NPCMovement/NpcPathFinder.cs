using UnityEngine;
using UnityEngine.AI;
public class NpcPathFinder : MonoBehaviour
{
    [SerializeField, Tooltip("The NavMeshAgent on this object.")] 
    protected NavMeshAgent _agent;
    [SerializeField, Tooltip("The Collider2D defining our aggroRange. Collider2Ds with a Targetable in this range can be targeted.")] 
    protected AggroRange aggroRange;

    // Our target affiliation. We can target targetables with other affiliations.
    protected TargetAffiliation ourAffiliation;
    // The game object we're currently targeting.
    protected Targetable target = null;
    // Accessor for the speed of our _agent.
    public float Speed {
        get { return _agent.speed; }
        set { _agent.speed = value; }
    }


    
    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        Targetable targetable = GetComponentInChildren<Targetable>();
        if (targetable != null) ourAffiliation = targetable.affiliation;
    }

    protected void SetDestination(Vector3 destination)
    {
        _agent.destination = destination;
    }

    protected virtual void Update()
    {
        if (target != null) 
        {
            SetDestination(target.transform.position);
        }
    }
}

// If we don't have a target...
        // if (target == null) 
        // {
        //     // Search for one!
        //     Targetable closest = null;
        //     float shortestDistance = float.MaxValue;
        //     foreach (Targetable targetable in aggroRange.TargetsInRange)
        //     {
        //         if (closest == null || )
        //     }
        // }