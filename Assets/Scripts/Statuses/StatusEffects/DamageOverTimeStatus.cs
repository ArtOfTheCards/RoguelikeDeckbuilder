using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Status/Damage Over Time")]
public class DamageOverTimeStatusFactory : StatusFactory<DamageOverTimeStatusData, DamageOverTimeStatusInstance> {}

[System.Serializable]
public class DamageOverTimeStatusData : StatusData
{
    [Header("DamageOverTime Parameters")]
    [Tooltip("The amount of damage done per tick. This amount is multiplied by the number of stacks.")]
    public int damagePerTick = 1;
    [Tooltip("The amount of time, in seconds, between damage ticks.")]
    public float tickDelay = 1f;
    [Tooltip("How long, in seconds, this status effect lasts.")]
    public float duration = 5f;
    [Tooltip("How much extra time, in seconds, we get when an additional stack is added.")]
    public float durationAddedOnStack = 1;
    public bool kirby = false;
}

public class DamageOverTimeStatusInstance : StatusInstance<DamageOverTimeStatusData>
{
    Coroutine damageOverTimeCoroutine = null;
    private Damagable damagable = null;
    float elapsed = 0;

    // ================================================================
    // Main methods
    // ================================================================

    public override void Apply()
    {
        if (target.TryGetComponent<Damagable>(out damagable))
        {
            damageOverTimeCoroutine = target.StartCoroutine(DamageOverTimeCoroutine());
        }
        else
        {
            Debug.Log("DamageOverTimeBuff: The target is invincible and has no health!", target);
        }
    }

    public override void AddAdditionalStack()
    {
        elapsed -= data.durationAddedOnStack;   // Give us more time
        base.AddAdditionalStack();
    }

    public override void End()
    {
        if (damageOverTimeCoroutine != null) target.StopCoroutine(damageOverTimeCoroutine);
        base.End();
    }

    // ================================================================
    // Additional methods
    // ================================================================

    public IEnumerator DamageOverTimeCoroutine()
    {
        float tickTimer = 0;
        while (elapsed < data.duration)
        {
            if (elapsed == 0 || tickTimer > data.tickDelay)
            {
                damagable.damage(data.damagePerTick * currentStacks);
                if (data.kirby is true)
                {
                    damagable.maxHealth = damagable.maxHealth-(data.damagePerTick * currentStacks);
                }
                tickTimer = 0;
            }
            
            elapsed += Time.deltaTime;
            tickTimer += Time.deltaTime;
            yield return null;
        }

        End();
    }
}