using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 0)]
public class Card : ScriptableObject
{
    public enum UseMode { NULL, Play, Throw }
    public enum TargetType { NULL, Direct, Worldspace }

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



    public void Use<T>(UseMode mode, T target)
    {
        if (mode == UseMode.NULL) {
            Debug.LogError("Card Error: Use failed. UseMode must not be NULL.");
            return;
        }

        List<CardEffect> effects = (mode == UseMode.Play) ? playEffects : throwEffects;

        foreach (CardEffect effect in effects)
        {
            // TODO: Give effects callbacks, only activate the next effect once the previous one has called back.

            // TODO: Thrown cards are projectiles out of the player. If it's first collision is with a targetable object,
            // it applies its effects to the targetable object. Otherwise, it does nothing.
            effect.Activate(target);
        }
    }
}

// Tasks -
// CardEffects should have a 'user' property for the CardUser that applied them, used for callbacks and other things
// CardEffects should have a list of 'condition' properties, where the condition only applies if all conditions are true

// ⭐Multitargeting: Different effects in the same list can prompt different targets

// ⭐EtchEffect: calls Display() on an EtchManager, applies the status effect recieved on callback
// ⭐Auto_MoveCardEffect: moves n cards of this CardUser from pile to pile
// ⭐DamageEffect: deals damage onto a Damageable
// ⭐StatusEffect: adds a status effect plainly --- status effects have bool for isEtchedStatus
// ⭐ConvertEtchStatusesEffect: removes all N etched effects, adds N stacks of a single other status effect
// ⭐SpawnEffect - summons a prefabbed object
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
// AllyCondition - returns true if number of CardUser allies of [type] are [==, <, <=, >, >=] to [number]
// StatusCondition - returns true if target has status
// EtchStatusCondition - returns true if target has any etched status