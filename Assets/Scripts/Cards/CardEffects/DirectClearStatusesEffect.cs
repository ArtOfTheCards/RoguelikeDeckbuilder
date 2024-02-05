using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

[System.Serializable]
public class DirectClearStatusesEffect : CardEffect
{
    public enum ClearType {All, List}
    // ================================================================
    // Parameters
    // ================================================================
    public ClearType clearType; 
    [Header("Used if ClearType is List")]
    public List<StatusFactory> statusesToClear = new();
    
    // ================================================================
    // Constructor
    // ================================================================
    public DirectClearStatusesEffect() { Debug_ID = "New DirectClearStatuses Effect"; }

    // ================================================================
    // Activate methods
    // ================================================================

    public override void Activate(CardUser caller, Card card, Targetable target)
    {
        // DIRECT target
        //
        // parameter caller: The CardUser which played the Card which this effect lives on.
        // parameter card: The Card which this effect lives on.
        // parameter target: The Targetable component on the Entity we are targeting.
        //      To access other scripts on the Entity, use target.gameObject.GetComponent<your target script>();
        // ================

        if (target.TryGetComponent<Effectable>(out Effectable effectable))
        {
            if (clearType == ClearType.All)
            {
                effectable.RemoveAllStatuses();
            }
            else if (clearType == ClearType.List)
            {
                effectable.RemoveSomeStatuses(statusesToClear);
            }
        }
        else
        {
            Debug.Log("DirectClearStatusEffect: Target is immune to status effects!");
        }

        EndEffect(card);
    }
}