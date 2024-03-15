using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;
using Cinemachine;  

public class Effectable : MonoBehaviour
{
    [HideInInspector] public List<StatusInstance> statuses = new();
    [SerializeField, ReadOnly] List<string> statusInspectorDebug = new();
    [SerializeField, Tooltip("Whether or not this Effectable should have a statusbar created for it at runtime.\n\nDefault: true")]
    bool createStatusbar = true;

    [HideInInspector] public bool changedSinceLastFrame = false;
    private Transform worldspaceCanvasTransform = null;
    private WorldspaceStatusbars worldspaceStatusbars;


    private void Awake()
    {
        // Gets our needed canvas UI references.
        // ================

        worldspaceCanvasTransform = GameObject.FindGameObjectWithTag("WorldspaceIndicators").transform;
        if (worldspaceCanvasTransform == null)
        {
            Debug.LogError("Effectable error: Awake failed. The scene has no indicator canvas, or the indicator canvas is not tagged as \"IndicatorCanvas\"");
        }
        else
        {
            worldspaceStatusbars = worldspaceCanvasTransform.GetComponentInChildren<WorldspaceStatusbars>();
        }
    }

    private void Start()
    {
        if (worldspaceStatusbars != null && createStatusbar)
        {
            worldspaceStatusbars.CreateStatusbar(this);
        }
    }

    private void OnDestroy()
    {
        if (worldspaceStatusbars != null && createStatusbar)
        {
            worldspaceStatusbars.DeleteStatusbar(this);
        }
    }

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
            changedSinceLastFrame = true;
        }
        else if (existingInstance.GetStatusData().stackable)    // if the status effect DOES exist and can stack.
        {
            statusInspectorDebug.Remove($"{existingInstance} ({existingInstance.currentStacks})");
            existingInstance.AddAdditionalStack();
            statusInspectorDebug.Add($"{existingInstance} ({existingInstance.currentStacks})");
            changedSinceLastFrame = true;
        }
    }

    public void RemoveStatusEffect(StatusInstance instance)
    {
        if (statuses.Contains(instance)) 
        {
            instance.End();
            statuses.Remove(instance);
            statusInspectorDebug.Remove($"{instance} ({instance.currentStacks})");
            changedSinceLastFrame = true;
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

    public void MarkChangeRepainted()
    {
        changedSinceLastFrame = false;
    }

    void OnGUI()
    {
        GUIStyle ourStyle = new(GUI.skin.box);
        ourStyle.fontSize = 35;

        for (int i=0; i<statusInspectorDebug.Count; i++)
        {
            GUI.Box(new Rect(Screen.width/2-150, 10+(i*60), 300, 50), statusInspectorDebug[i], ourStyle);
        }
    }

}