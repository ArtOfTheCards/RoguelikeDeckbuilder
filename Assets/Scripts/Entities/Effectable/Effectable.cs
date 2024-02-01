using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public class Effectable : MonoBehaviour
{
    private List<StatusInstance> statuses = new();
    [SerializeField, ReadOnly] List<string> statusInspectorDebug = new();

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
            Debug.LogError($"Effectable Error: RemoveStatusEffect failed. Creature does not have status {instance}");
            return;
        }

        instance.statusEnded -= RemoveStatusEffect;
    }

    public void RemoveAllStatuses()
    {
        foreach (StatusInstance instance in statuses.ToArray())
        {
            RemoveStatusEffect(instance);
            statusInspectorDebug.Remove(instance.ToString());
        }
    }

    public void RemoveStatusesOfType(StatusFactory status)
    {
        foreach (StatusInstance instance in statuses.ToArray())
        {
            if (status.Matches(instance))   // Checks that type AND ID are the same.
            {
                RemoveStatusEffect(instance);
                statusInspectorDebug.Remove(instance.ToString());
            }
        }
    }
}