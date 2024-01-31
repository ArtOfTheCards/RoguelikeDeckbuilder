using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 0)]
public class Card : ScriptableObject
{
    public enum UseMode { NULL, Play, Throw }
    public enum TargetType { NULL, Direct, Worldspace, Targetless }

    [Tooltip("In-code name for this card. Not shown in-game.")]
    public string debug_ID = "New ID";
    [Tooltip("In-game name for this card. Displayed on the card object in-game.")]
    public string title = "New Card";
    [Tooltip("Card art to be displayed on the in-game card object.")]
    public Sprite art;


    public TargetType playTarget = TargetType.NULL;
    public string playDescription;
    [SerializeReference]
    public List<CardEffect> playEffects = new();



    public TargetType throwTarget = TargetType.NULL;
    public string throwDescription;
    [SerializeReference]
    public List<CardEffect> throwEffects = new();



    private bool effectCalledback = false;



    // ================================================================
    // External use methods
    // ================================================================

    public void Use(CardUser caller, UseMode mode, Targetable target)
    {
        // TARGETABLE target: Activates the list of CardEffects corresponding to mode.
        // ================

        if (ValidateUse(mode) == false) return;

        List<CardEffect> effects = (mode == UseMode.Play) ? playEffects : throwEffects;
        caller.StartCoroutine(ApplyEffects_Targetable(caller, effects, target));
    }

    public void Use(CardUser caller, UseMode mode, Vector3 target)
    {
        // VECTOR3 target: Activates the list of CardEffects corresponding to mode.
        // ================

        if (ValidateUse(mode) == false) return;

        List<CardEffect> effects = (mode == UseMode.Play) ? playEffects : throwEffects;
        caller.StartCoroutine(ApplyEffects_Vector3(caller, effects, target));
    }

    public void Use(CardUser caller, UseMode mode)
    {
        // TARGETLESS target: Activates the list of CardEffects corresponding to mode.
        // ================

        if (ValidateUse(mode) == false) return;

        List<CardEffect> effects = (mode == UseMode.Play) ? playEffects : throwEffects;
        caller.StartCoroutine(ApplyEffects_Targetless(caller, effects));
    }

    private static bool ValidateUse(UseMode mode)
    {
        if (mode == UseMode.NULL)
        {
            Debug.LogError("Card Error: Use failed. UseMode must not be NULL.");
            return false;
        }
        return true;
    }

    // ================================================================
    // Effect application methods
    // ================================================================

    private IEnumerator ApplyEffects_Targetable(CardUser caller, List<CardEffect> effects, Targetable target)
    {
        effectCalledback = false;

        foreach (CardEffect effect in effects)
        {
            effect.Activate(caller, this, target);
            yield return new WaitUntil(() => effectCalledback == true);
            effectCalledback = false;         
        }
        yield return null;
    }

    private IEnumerator ApplyEffects_Vector3(CardUser caller, List<CardEffect> effects, Vector3 target)
    {
        effectCalledback = false;

        foreach (CardEffect effect in effects)
        {
            effect.Activate(caller, this, target);
            yield return new WaitUntil(() => effectCalledback == true);
            effectCalledback = false;         
        }
        yield return null;
    }

    private IEnumerator ApplyEffects_Targetless(CardUser caller, List<CardEffect> effects)
    {
        effectCalledback = false;

        foreach (CardEffect effect in effects)
        {
            effect.Activate(caller, this);
            yield return new WaitUntil(() => effectCalledback);
            effectCalledback = false;         
        }
        yield return null;
    }

    public void EffectFinished()
    {
        effectCalledback = true;
    }
}

// Tasks -

// ⭐EtchEffect: calls Display() on an EtchManager, applies the status effect recieved on callback
// ⭐StatusEffect: adds a status effect plainly --- status effects have bool for isEtchedStatus
// ⭐ConvertEtchStatusesEffect: removes all N etched effects, adds N stacks of a single other status effect
//
// ⭐FlatStrength Status - increase the power of attack cards by the number of stacks (strength 2- cards deal 2 more damage) 
//      - should work in the negative direction also
// ⭐PercentStrength Status - increase the power of attack cards by a percentage, applied after any FlatStrength effects 
//      - should work in the negative direction also
// ⭐PercentMoveSpeed Status - this entity moves differently by a percentage of base speed
// ⭐PercentVulnerable Status - attacks targeting this entity deal a percentage more damage 

// LOW PRIORITY
// GiveCardEffect: inserts a list of cards into a given pile on a CardUser
// Rattle Status - freeze the draw timer on a CardUser for a duration
//
// CardEffects should have a list of 'condition' properties, where the condition only applies if all conditions are true
// AllyCondition - returns true if number of CardUser allies of [type] are [==, <, <=, >, >=] to [number]
// StatusCondition - returns true if target has status
// EtchStatusCondition - returns true if target has any etched status