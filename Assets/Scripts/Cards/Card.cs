using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class Dummy_StatusEffect 
{ 
    public string Debug_ID;
    public Dummy_StatusEffect(string _ID) { Debug_ID = _ID; }
    public void Apply() { Debug.Log($"Effect: {Debug_ID}"); } 
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Card", menuName = "Custom/Card")]
public class Card : ScriptableObject
{
    [BoxGroup("Base Info"), Tooltip("In-code name for this card. Not shown in-game.")]
    public string debug_ID = "New ID";
    [BoxGroup("Base Info"), Tooltip("In-game name for this card. Displayed on the card object in-game.")]
    public string title = "New Card";
    [BoxGroup("Base Info"), Tooltip("Card art to be displayed on the in-game card object.")]
    public Sprite art;



    [BoxGroup("On Play"), Tooltip("In-game description of this card's play effect.")]
    public string descriptionPlayed = "OnPlay text.";
    [BoxGroup("On Play"), Tooltip("A list of status effects applied to all targets when this card is played.")]
    public Dummy_StatusEffect[] effectsPlayed;



    [BoxGroup("On Throw"), Tooltip("In-game description of this card's thrown effect.")]
    public string descriptionThrown = "OnThrow text.";
    [BoxGroup("On Throw"), Tooltip("A list of status effects applied to all targets when this card is thrown.")]
    public Dummy_StatusEffect[] effectsThrown;

    // ================================================================
    // ... methods
    // ================================================================

    public virtual void Play() 
    { 
        //Debug.Log($"Played {debug_ID}");
        foreach (Dummy_StatusEffect effect in effectsPlayed)
        {
            effect.Apply();
        }
    }

    public virtual void Throw() 
    { 
        Debug.Log($"Played {debug_ID}");
        foreach (Dummy_StatusEffect effect in effectsThrown)
        {
            effect.Apply();
        }
    }

    public override string ToString() { return debug_ID; }
}