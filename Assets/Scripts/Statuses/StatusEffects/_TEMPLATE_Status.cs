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

//[CreateAssetMenu(menuName = "Status/TEMPLATE")]
public class TEMPLATEStatusFactory : StatusFactory<TEMPLATEStatusData, TEMPLATEStatusInstance> {}



[System.Serializable]
public class TEMPLATEStatusData : StatusData
{
    // Data class for the Status. Any global parameters, like duration or intensity, should go here.
    // ================

    //[Header("TEMPLATE Parameters")]
}



public class TEMPLATEStatusInstance : StatusInstance<TEMPLATEStatusData>
{
    // Instance class for the Status. Any local parameters, like timers or other state, should go here.
    // Also, any code that determines how the status works should go here.
    //
    // To access the Data class, use data.[Property Name].
    // To access the number of instance stacks, use currentStacks. 
    // ================

    // private Coroutine TEMPLATERoutine = null;                                // UNCOMMENT this line if you use TEMPLATECoroutine().

    // ================================================================
    // Main methods
    // ================================================================

    public override void Apply()
    {
        // IMPORTANT:
        // Apply() is called when a status effect is applied to an Effectable. If Apply() is left empty,
        // this status will do nothing.

        // ====================================
        // ==== Meaningful code goes here. ====
        // ====================================

        // TEMPLATERoutine = target.StartCoroutine(TEMPLATECoroutine());        // UNCOMMENT this line if you use TEMPLATECoroutine().
    }

    public override void AddAdditionalStack()
    {
        base.AddAdditionalStack();
    }

    public override void End()
    {
        //if (TEMPLATERoutine != null) target.StopCoroutine(TEMPLATERoutine);   // UNCOMMENT this line if you use TEMPLATECoroutine().
        base.End();
    }

    // ================================================================
    // Additional methods
    // ================================================================

    /*public IEnumerator TEMPLATECoroutine()
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