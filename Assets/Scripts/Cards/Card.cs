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
            effect.Activate(target);
        }
    }
}