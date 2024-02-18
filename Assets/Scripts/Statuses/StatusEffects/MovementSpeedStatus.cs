using UnityEngine;
using System.Collections;

// README:
// This file contains template classes needed to make a new status. To use this file...
// 1. Create a copy and name it [Your Status Name]Status.cs
// 2. Use find-and-replace (Ctrl-H on Windows, Cmd-H on Mac) to replace "TEMPLATE" with [Your Status Name]
// 3. Add any global parameter data (duration, intensity, whatever) into [Your Status Name]StatusData.
// 4. Add the behavior of your status into [Your Status Name]StatusInstance. Make sure Apply() isn't empty!
//    Otherwise, your status won't do anything.
// 5. Uncomment the [CreateAssetMenu ...] tag on line 14.
// 6. To create your status factory object, right click in the Project tab, and select Create > Status > [Your Status Name].

[CreateAssetMenu(menuName = "Status/MovementSpeed")]
public class MovementSpeedStatusFactory : StatusFactory<MovementSpeedStatusData, MovementSpeedStatusInstance> {}



[System.Serializable]
public class MovementSpeedStatusData : StatusData
{
    // Data class for the Status. Any global parameters, like duration or intensity, should go here.
    // ================
    [Header("MovementSpeed Parameters")]
    [Tooltip("The change of speed in percentage")]
    public float speedChangePercent = 0;
    [Tooltip("How long, in seconds, this status effect lasts.")]
    public float duration = 5f;
    [Tooltip("The duration added to this effect, per stack.")]
    public float durationAddedOnStack = 0;

    //[Header("TEMPLATE Parameters")]
}



public class MovementSpeedStatusInstance : StatusInstance<MovementSpeedStatusData>
{
    // Instance class for the Status. Any local parameters, like timers or other state, should go here.
    // Also, any code that determines how the status works should go here.
    //
    // To access the Data class, use data.[Property Name].
    // To access the number of instance stacks, use currentStacks. 
    // ================
    private NpcPathFinder npc = null;
    private float baseSpeed;
    private bool subscribed = false;
    private float elapsed = 0;
    private Coroutine endRoutine = null;

    // ================================================================
    // Main methods
    // ================================================================

    public override void Apply()
    {
        if (target.TryGetComponent<NpcPathFinder>(out npc))
        { 
            baseSpeed = npc.GetSpeed();
            npc.SetSpeed(baseSpeed*(1+(data.speedChangePercent*.01f)));
            endRoutine = target.StartCoroutine(EndCoroutine());
        }
        else
        {
            Debug.Log("MovementSpeedStatus: ashley did this wrong!!!", target);
            End();
        }
    }

    public override void AddAdditionalStack()
    {
        elapsed -= data.durationAddedOnStack;   // Give us more time
        base.AddAdditionalStack();
    }

    public override void End()
    {
        if (endRoutine != null) target.StopCoroutine(EndCoroutine());
        if (npc != null && baseSpeed != 0) npc.SetSpeed(baseSpeed);
        base.End();
    }

    // ================================================================
    // Additional methods
    // ================================================================

    public IEnumerator EndCoroutine()
    {
        while (elapsed < data.duration)
        {
            yield return null;
            elapsed += Time.deltaTime;
        }

        End();
    }
}