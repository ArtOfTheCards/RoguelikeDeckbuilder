using UnityEngine;

[System.Serializable]
/* 
   Replace "_TEMPLATE_CardEffect" with the name of your effect. 
*/
public class AutoRandomDiscardEffect : CardEffect
{
    // ================================================================
    // Parameters
    // ================================================================
    /*
       Put any public parameters here.
    */
    
    // ================================================================
    // Internal variables
    // ================================================================
    /*
       Put any private variables here.
    */
    
    // ================================================================
    // Constructor
    // ================================================================
    /* 
       Replace "_TEMPLATE_CardEffect" with the name of your effect. 
    */
    public AutoRandomDiscardEffect() { Debug_ID = "New AutoRandomDiscard Effect"; }

    // ================================================================
    // Activate methods
    // ================================================================
    /* 
       There are multiple ways to Activate() a card, each of which corresponds to
       a different Card TargetType- either Direct, Worldspace, or Targetless.

       Fill in as many of these effects as make sense. For example, the SpawnEffect,
       which spawns a gameObject, defines behavior for Worldspace (spawn at the target)
       and Direct (spawn at the target's *position*), but NOT targetless (you can't
       spawn something at nowhere).

       Meanwhile, the DelayEffect makes sense for all three TargetTypes. You can 
       delay any kind of effect, so we implement all three versions of Activate()!

       If a version of Activate() doesn't make sense to implement, delete it from this file.
    */
    public override void Activate(CardUser caller, Card card, Targetable target)
    {
        // DIRECT target
        //
        // parameter caller: The CardUser which played the Card which this effect lives on.
        // parameter card: The Card which this effect lives on.
        // parameter target: The Targetable component on the Entity we are targeting.
        //      To access other scripts on the Entity, use target.gameObject.GetComponent<your target script>();
        // ================

        /* 
           ALWAYS include EndEffect(card); at the end of Activate()!
        */
        EndEffect(card);
    }

    public override void Activate(CardUser caller, Card card, Vector3 target)
    {
        // WORLDSPACE target
        //
        // parameter caller: The CardUser which played the Card which this effect lives on.
        // parameter card: The Card which this effect lives on.
        // parameter target: A Vector3 representing the position in worldspace of our target.
        // ================

        /* 
           ALWAYS include EndEffect(card); at the end of Activate()!
        */
        EndEffect(card);
    }

    public override void Activate(CardUser caller, Card card)
    {
        // TARGETLESS target
        //
        // parameter caller: The CardUser which played the Card which this effect lives on.
        //      To access other scripts on the same Entity as our caller, use caller.gameObject.GetComponent<your target script>();
        // parameter card: The Card which this effect lives on.
        // ================
        if (caller.hand.Count > 1)
        {
            Card target = card;
            do
            {
                target = caller.GetRandom(CardPile.hand);
            } while (target == card);
            caller.Discard(target);
        }
        /* 
           ALWAYS include EndEffect(card); at the end of Activate()!
        */
        EndEffect(card);
    }

    /* 
       Once you're all done, remove any comments between / and *, like this one!
    */
}