using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Status/Explode")]
public class ExplodeStatusFactory : StatusFactory<ExplodeStatusData, ExplodeStatusInstance> {}

[System.Serializable]
public class ExplodeStatusData : StatusData
{
    [Header("Explode Parameters")]
    [Tooltip("How long, in seconds, this status effect lasts.")]
    public float duration = 5f;
    [Tooltip("The amount of damage dealt at the end of this effect, per stack.")]
    public int damagePerStack = 5;
    [Tooltip("How much extra time, in seconds, we get when an additional stack is added.")]
    public float durationAddedOnStack = 0;
}

public class ExplodeStatusInstance : StatusInstance<ExplodeStatusData>
{
    private Coroutine endRoutine = null;
    private Damagable damagable = null;
    float elapsed = 0;

    // ================================================================
    // Main methods
    // ================================================================

    public override void Apply()
    {
        if (target.TryGetComponent<Damagable>(out damagable))
        {
            endRoutine = target.StartCoroutine(EndCoroutine());
        }
        else
        {
            Debug.Log("ExplodeStatus: The target is invincible and has no health!", target);
        }
    }

    public override void AddAdditionalStack()
    {
        elapsed -= data.durationAddedOnStack;   // Give us more time
        base.AddAdditionalStack();
    }

    public override void End()
    {
        if (endRoutine != null) target.StopCoroutine(endRoutine);
        base.End();
    }

    // ================================================================
    // Additional methods
    // ================================================================

    public IEnumerator EndCoroutine()
    {
        
        while (elapsed < data.duration)
        { 
            elapsed += Time.deltaTime;
            yield return null;
        }

        damagable.damage(data.damagePerStack * currentStacks);
        End();
    }
}