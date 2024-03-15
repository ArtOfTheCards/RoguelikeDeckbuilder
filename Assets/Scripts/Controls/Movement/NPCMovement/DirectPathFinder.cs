using UnityEngine;

public class DirectPathFinder : NpcPathFinder
{
    [SerializeField, Tooltip("The amount of time, in seconds, after our target leaves aggro range, before we lose aggro.\n\nDefault: 4")]
    private float loseAggroDelay = 4;
    private float loseAggroElapsed = 0;

    
    protected override void Update()
    {
        // If we don't have a target...
        if (target == null) 
        {
            // Search for one!
            Targetable closest = null;
            float shortestDistance = float.MaxValue;
            foreach (Targetable targetable in aggroRange.TargetsInRange)
            {
                // If the target is a valid one AND it is the closest targetable so far...
                if (targetable.affiliation != ourAffiliation
                && (closest == null || Vector3.Distance(transform.position, targetable.transform.position) < shortestDistance))
                {
                    shortestDistance = Vector3.Distance(transform.position, targetable.transform.position);
                    closest = targetable;
                }
            }

            target = closest;
        }

        if (target != null) 
        {
            SetDestination(target.transform.position);
            
            if (!aggroRange.TargetsInRange.Contains(target))  // Target has left range.
            {
                if (loseAggroElapsed >= loseAggroDelay) 
                {
                    target = null;
                    loseAggroElapsed = 0;
                }
                else
                {
                    loseAggroElapsed += Time.deltaTime;
                }
            }
            
        }
    }
}
        