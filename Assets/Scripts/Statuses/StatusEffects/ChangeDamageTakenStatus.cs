using UnityEngine;
using NaughtyAttributes;
using System.Collections;



[CreateAssetMenu(menuName = "Status/Change Damage Taken")]
public class ChangeDamageTakenStatusFactory : StatusFactory<ChangeDamageTakenStatusData, ChangeDamageTakenStatusInstance> {}



[System.Serializable]
public class ChangeDamageTakenStatusData : StatusData
{
    // Data class for the Status. Any global parameters, like duration or intensity, should go here.
    // ================

    [Header("ChangeDamageTaken Parameters")]
    public StatModifier damageTakenModifier = new();
    [Tooltip("How long, in seconds, this status effect lasts.")]
    public float duration = 4;
    [Tooltip("How much extra time, in seconds, we get when an additional stack is added.")]
    public float durationAddedOnStack = 1;
}



public class ChangeDamageTakenStatusInstance : StatusInstance<ChangeDamageTakenStatusData>
{
    // Instance class for the Status. Any local parameters, like timers or other state, should go here.
    // Also, any code that determines how the status works should go here.
    // ================

    private Damagable damagable = null;
    private bool subscribed = false;
    private float elapsed = 0;
    private Coroutine endRoutine = null;

    // ================================================================
    // Main methods
    // ================================================================

    public override void Apply()
    {
        if (target.TryGetComponent<Damagable>(out damagable))
        {
            damagable.OnCalculateDamage += ModifyDamage;
            subscribed = true;
            endRoutine = target.StartCoroutine(EndCoroutine());
        }
        else
        {
            Debug.Log("ChangeDamageTakenStatus: The target is invincible and can't take damage!", target);
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
        if (endRoutine != null) target.StopCoroutine(endRoutine);
        if (subscribed) damagable.OnCalculateDamage -= ModifyDamage;

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

    public void ModifyDamage(StatModifierBank modifiers)
    {
        // Adds our modifier to the damagable's modifier bank.
        // ================

        modifiers.AddModifier(data.damageTakenModifier, currentStacks);
    }
}