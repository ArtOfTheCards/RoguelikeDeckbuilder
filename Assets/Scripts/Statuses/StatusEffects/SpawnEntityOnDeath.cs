using UnityEngine;
using System.Collections;

// README:
// This file contains SpawnEntityOnDeath classes needed to make a new status. To use this file...
// 1. Create a copy and name it [Your Status Name]Status.cs
// 2. Use find-and-replace (Ctrl-H on Windows, Cmd-H on Mac) to replace "SpawnEntityOnDeath" with [Your Status Name]
// 3. Add any global parameter data (duration, intensity, whatever) into [Your Status Name]StatusData.
// 4. Add the behavior of your status into [Your Status Name]StatusInstance. Make sure Apply() isn't empty!
//    Otherwise, your status won't do anything.
// 5. Uncomment the [CreateAssetMenu ...] tag on line 14.
// 6. To create your status factory object, right click in the Project tab, and select Create > Status > [Your Status Name].

[CreateAssetMenu(menuName = "Status/SpawnEntityOnDeath")]
public class SpawnEntityOnDeathStatusFactory : StatusFactory<SpawnEntityOnDeathStatusData, SpawnEntityOnDeathStatusInstance> {}



[System.Serializable]
public class SpawnEntityOnDeathStatusData : StatusData
{
    [Header("SpawnEntityOnDeath Parameters")]
    // Data class for the Status. Any global parameters, like duration or intensity, should go here.
    // ================
    public GameObject toSpawn;
}



public class SpawnEntityOnDeathStatusInstance : StatusInstance<SpawnEntityOnDeathStatusData>
{
    // Instance class for the Status. Any local parameters, like timers or other state, should go here.
    // Also, any code that determines how the status works should go here.
    //
    // To access the Data class, use data.[Property Name].
    // To access the number of instance stacks, use currentStacks. 
    // ================
    private Damagable damagable = null;
    

    // private Coroutine SpawnEntityOnDeathRoutine = null;                                // UNCOMMENT this line if you use SpawnEntityOnDeathCoroutine().

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
        
        damagable.deathTrigger += OnDeath; // 'subscribes' to our action

        // SpawnEntityOnDeathRoutine = target.StartCoroutine(SpawnEntityOnDeathCoroutine());        // UNCOMMENT this line if you use SpawnEntityOnDeathCoroutine().
    }

    public void OnDeath() 
    {
        for(int i = 0; i < currentStacks; i++)
        {
            GameObject.Instantiate(data.toSpawn,target.transform.position, Quaternion.identity);
        }
        damagable.deathTrigger -= OnDeath; // 'unsubscribes', we get bugs if we don't do this 
    }

    public override void AddAdditionalStack()
    {
        base.AddAdditionalStack();
    }

    public override void End()
    {
        //if (SpawnEntityOnDeathRoutine != null) target.StopCoroutine(SpawnEntityOnDeathRoutine);   // UNCOMMENT this line if you use SpawnEntityOnDeathCoroutine().
        base.End();
    }

    // ================================================================
    // Additional methods
    // ================================================================

    /*public IEnumerator SpawnEntityOnDeathCoroutine()
    {
        // SpawnEntityOnDeath coroutine code for making something happen over time.

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