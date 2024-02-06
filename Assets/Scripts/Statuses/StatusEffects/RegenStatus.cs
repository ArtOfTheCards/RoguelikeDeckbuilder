using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Status/Regen")]
public class RegenStatusFactory : StatusFactory<RegenStatusData, RegenStatusInstance> {}

[System.Serializable]
public class RegenStatusData : StatusData
{
    [Header("Regen Parameters")]
    public int healthPerTick = 1;
    public float tickDelay = 1f;
    public float duration = 5f;
    [Tooltip("How much extra time, in seconds, we get when an additional stack is added.")]
    public float durationAddedOnStack = 1;
}

public class RegenStatusInstance : StatusInstance<RegenStatusData>
{
    Coroutine regenCoroutine = null;
    private Damagable damagable = null;
    float elapsed = 0;

    // ================================================================
    // Main methods
    // ================================================================

    public override void Apply()
    {
        if (target.TryGetComponent<Damagable>(out damagable))
        {
            regenCoroutine = target.StartCoroutine(RegenCoroutine());
        }
        else
        {
            Debug.Log("RegenBuff: The target is invincible and has no health!", target);
        }
    }

    public override void AddAdditionalStack()
    {
        elapsed -= data.durationAddedOnStack;   // Give us more time
        base.AddAdditionalStack();
    }

    public override void End()
    {
        if (regenCoroutine != null) target.StopCoroutine(regenCoroutine);
        base.End();
    }

    // ================================================================
    // Additional methods
    // ================================================================

    public IEnumerator RegenCoroutine()
    {
        float tickTimer = 0;
        while (elapsed < data.duration)
        {
            if (elapsed == 0 || tickTimer > data.tickDelay)
            {
                damagable.heal(data.healthPerTick * currentStacks);
                tickTimer = 0;
            }
            
            elapsed += Time.deltaTime;
            tickTimer += Time.deltaTime;
            yield return null;
        }

        End();
    }
}