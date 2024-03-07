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

[CreateAssetMenu(menuName = "Status/ChangeSpriteSizeStatus")]
public class ChangeSpriteSizeStatusStatusFactory : StatusFactory<ChangeSpriteSizeStatusStatusData, ChangeSpriteSizeStatusStatusInstance> {}



[System.Serializable]
public class ChangeSpriteSizeStatusStatusData : StatusData
{
    // Data class for the Status. Any global parameters, like duration or intensity, should go here.
    // ================
    [Header("Size Change")]
    [Tooltip("The change of size in percentage")]
    public float sizeChangePercent = 0;
    [Tooltip("How long, in seconds, this status effect lasts.")]
    public float duration = 5f;
    [Tooltip("The duration added to this effect, per stack.")]
    public float durationAddedOnStack = 0;

    //[Header("TEMPLATE Parameters")]
}



public class ChangeSpriteSizeStatusStatusInstance : StatusInstance<ChangeSpriteSizeStatusStatusData>
{
    // Instance class for the Status. Any local parameters, like timers or other state, should go here.
    // Also, any code that determines how the status works should go here.
    //
    // To access the Data class, use data.[Property Name].
    // To access the number of instance stacks, use currentStacks. 
    // ================
    private NpcPathFinder npc = null;
    private Vector3 baseSize;
    private float elapsed = 0;
    //private Coroutine endRoutine = null;
    
    // ================================================================
    // Main methods
    // ================================================================

    public override void Apply()
    {
        // IMPORTANT:
        // Apply() is called when a status effect is applied to an Effectable. If Apply() is left empty,
        // this status will do nothing.
        

        if (target.TryGetComponent<NpcPathFinder>(out npc))
        { 
            Vector3 sizeChangeVector;
            Vector3 baseSize;
            Vector3 transformVector;
            sizeChangeVector = new Vector3(data.sizeChangePercent,data.sizeChangePercent,data.sizeChangePercent);
            baseSize = npc.transform.localScale;
            Debug.Log(npc.transform.localScale);
            transformVector = new Vector3(baseSize[0]*sizeChangeVector[0], baseSize[1]*sizeChangeVector[1], baseSize[2]*sizeChangeVector[2]);
            npc.transform.localScale = transformVector;
            Debug.Log(npc.transform.localScale);
            /*baseSize = npc.GetSize();
            npc.SetSize(baseSize*(1+(data.sizeChangePercent*.01f)));*/
            //endRoutine = target.StartCoroutine(EndCoroutine());
        }
        else
        {
            Debug.Log("ChangeSpriteSizeStatusStatus: ashley did this wrong!!!", target);
            End();
        }

        //ChangeSpriteSizeStatusStatusRoutine = target.StartCoroutine(ChangeSpriteSizeStatusStatusCoroutine());        // UNCOMMENT this line if you use TEMPLATECoroutine().
    }

    public override void AddAdditionalStack()
    {
        elapsed -= data.durationAddedOnStack;   // Give us more time
        base.AddAdditionalStack();
    }

    public override void End()
    {
        //if (endRoutine != null) target.StopCoroutine(EndCoroutine());
        if (npc != null) npc.transform.localScale = baseSize;
        base.End();
    }

    // ================================================================
    // Additional methods
    // ================================================================

    /*public IEnumerator ChangeSpriteSizeStatusStatusCoroutine()
    {
        // Template coroutine code for making something happen over time.

        float elapsed = 0;
        while (elapsed < data.duration)
        {
            // ====================================
            // ==== Meaningful code goes here. ====
            // ====================================

            elapsed += Time.deltaTime;
            yield return null;
        }
    }*/
}