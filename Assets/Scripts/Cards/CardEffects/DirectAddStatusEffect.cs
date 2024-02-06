using UnityEngine;

[System.Serializable]
/* 
   Replace "DirectAddStatusEffect" with the name of your effect. 
*/
public class DirectAddStatusEffect : CardEffect
{
    // ================================================================
    // Parameters
    // ================================================================
    public StatusFactory status;
    
    // ================================================================
    // Constructor
    // ================================================================
    public DirectAddStatusEffect() { Debug_ID = "New DirectAddStatus Effect"; }

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
            effectable.AddStatusEffect(status);
        }
        else
        {
            Debug.Log("DirectAddStatusEffect: Target is immune to status effects!");
        }

        EndEffect(card);
    }
}