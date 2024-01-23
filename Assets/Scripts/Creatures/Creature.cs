using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public class Creature : MonoBehaviour
{
    public float health;
    public float attack;
    public List<CreatureTag> tags;

    private List<StatusInstance> statuses = new();
    [SerializeField, ReadOnly] List<string> statusInspectorDebug = new();

    private void Update()
    {
        // Update is called once per frame.
        // ================
    }

    // ================================================================
    // Stats maintenance
    // ================================================================

    public void ChangeHealth(float amount)
    {
        print($"Changed health by {amount}. Health was {health}, now {health+amount}");
        health += amount;
    }

    public void ChangeAttack(float amount)
    {
        print($"Changed attack by {amount}. Attack was {attack}, now {attack+amount}");
        attack += amount;
    }

    // ================================================================
    // Status effect maintenance
    // ================================================================    

    public void AddStatusEffect(StatusFactory status)
    {
        StatusInstance instance = status.CreateStatusInstance(this);
        
        statuses.Add(instance);
        statusInspectorDebug.Add(instance.ToString());

        instance.statusEnded += RemoveStatusEffect;
        instance.Apply();
    }

    public void RemoveStatusEffect(StatusInstance instance)
    {
        if (statuses.Contains(instance)) 
        {
            instance.End(prematurely:true); // Ending an effect prematurely prevents it from calling this function infinitely.
            statuses.Remove(instance);
            statusInspectorDebug.Remove(instance.ToString());
        }
        else
        {
            Debug.LogError($"Creature Error: RemoveStatusEffect failed. Creature does not have status {instance}");
            return;
        }
        
        instance.statusEnded -= RemoveStatusEffect;
    }

    public void RemoveAllStatuses()
    {
        foreach (StatusInstance instance in statuses.ToArray())
        {
            RemoveStatusEffect(instance);
        }
    }

    public void RemoveStatusesOfType(StatusFactory status)
    {
        foreach (StatusInstance instance in statuses.ToArray())
        {
            if (status.Matches(instance))
            {
                RemoveStatusEffect(instance);
            }
        }
    }
}
