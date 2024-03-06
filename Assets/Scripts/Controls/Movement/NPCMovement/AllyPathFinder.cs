using UnityEngine;
using NaughtyAttributes;

public class AllyPathFinder : NpcPathFinder
{
    [SerializeField, Tooltip("The amount of time, in seconds, after our target leaves aggro range, before we lose aggro.\n\nDefault: 4")]
    private float loseAggroDelay = 4;
    [SerializeField, Tooltip("The object we follow if there are no enemies in range.")]
    private Targetable boss;
    private float loseAggroElapsed = 0;

    protected override void Awake()
    {
        aggroRange.updated += UpdateTarget;
        base.Awake();
    }

    protected void OnDestroy()
    {
        aggroRange.updated -= UpdateTarget;
    }

    public void InitializeBoss(Targetable _boss)
    {
        boss = _boss;
        target = boss;
    }

    private void UpdateTarget()
    {
        // If the old target was our boss, check to see if any enemies have entered.
        if (target == boss)
        {
            Targetable closest = null;
            float shortestDistance = float.MaxValue;
            foreach (Targetable targetable in aggroRange.TargetsInRange)
            {
                // If the target is a valid one AND it is the closest targetable so far...
                if (targetable.affiliation != ourAffiliation
                && (closest == null || Vector3.Distance(transform.position, targetable.transform.position) < shortestDistance))
                {
                    // Mark it.
                    shortestDistance = Vector3.Distance(transform.position, targetable.transform.position);
                    closest = targetable;
                }
            }

            // If closest isn't null after our search, target them. Otherwise, target the boss.
            if (closest != null) 
            {
                target = closest;
                loseAggroElapsed = 0;
            }
        }
        
        // If the old target was an enemy and they're no longer in range...
        if (target.affiliation != ourAffiliation && !aggroRange.TargetsInRange.Contains(target))
        {
            // Start a countdown.
            if (loseAggroElapsed < loseAggroDelay)
            {
                loseAggroElapsed += Time.deltaTime;
            }
            else
            {
                // Once time has elapsed, look for a new target.
                loseAggroElapsed = 0;

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

                // If closest isn't null after our search, target them. Otherwise, target the boss.
                target = (closest != null) ? closest : boss;
            }
        }
    }
}
        