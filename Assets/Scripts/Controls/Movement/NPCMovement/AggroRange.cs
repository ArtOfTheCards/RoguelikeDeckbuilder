using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

public class AggroRange : MonoBehaviour
{
    [ReadOnly, SerializeField] private NpcPathFinder _npc;

    private HashSet<Targetable> targetsInRange = new();
    public HashSet<Targetable> TargetsInRange {
        get { return targetsInRange; }
    }

    public System.Action updated;

    void Awake()
    {
        _npc = GetComponentInParent<NpcPathFinder>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Targetable target = other.gameObject.GetComponentInChildren<Targetable>();
        if (target != null)
        {
            targetsInRange.Add(target);
            updated?.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Targetable target = other.gameObject.GetComponentInChildren<Targetable>();
        if (target != null)
        {
            targetsInRange.Remove(target);
            updated?.Invoke();
        }
    }
}