using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public class Effectable : MonoBehaviour
{
    private List<StatusInstance> statuses = new();
    [SerializeField, ReadOnly] List<string> statusInspectorDebug = new();

    public void AddStatusEffect(StatusFactory status)
    {
        StatusInstance existingInstance = GetStatusInstanceOfType(status);

        if (existingInstance == null)                           // if the status effect doesn't exist on this object.
        {
            StatusInstance instance = status.CreateStatusInstance(this);

            statuses.Add(instance);
            statusInspectorDebug.Add($"{instance} ({instance.currentStacks})");

            instance.statusEnded += RemoveStatusEffect;
            instance.Apply();
        }
        else if (existingInstance.GetStatusData().stackable)    // if the status effect DOES exist and can stack.
        {
            statusInspectorDebug.Remove($"{existingInstance} ({existingInstance.currentStacks})");
            existingInstance.AddAdditionalStack();
            statusInspectorDebug.Add($"{existingInstance} ({existingInstance.currentStacks})");
        }
    }

    public void RemoveStatusEffect(StatusInstance instance)
    {
        if (statuses.Contains(instance)) 
        {
            instance.End();
            statuses.Remove(instance);
            statusInspectorDebug.Remove($"{instance} ({instance.currentStacks})");
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
            statusInspectorDebug.Remove($"{instance} ({instance.currentStacks})");
        }
    }

    public void RemoveSomeStatuses(List<StatusFactory> statusFactories)
    {
        // TODO, TO-DO (maybe):
        // n^2 time complexity is not great! Try to make it easier to get the instances from the factories.
        // Maybe store instances as dictionary entries instead of in a list? That way instances can be indexed
        // from their factories. But this only works if we're POSITIVE we'll only have one instance per type.

        foreach (StatusInstance instance in statuses.ToArray())
        {
            foreach (StatusFactory factory in statusFactories)
            {
                if (factory.Matches(instance))
                {
                    RemoveStatusEffect(instance);
                    statusInspectorDebug.Remove($"{instance} ({instance.currentStacks})");
                }
            }
        }
    }

    public StatusInstance GetStatusInstanceOfType(StatusFactory status)
    {
        foreach (StatusInstance instance in statuses)
        {
            // Checks that type AND ID are the same.
            if (status.Matches(instance)) return instance;
        }

        return null;
    }
}