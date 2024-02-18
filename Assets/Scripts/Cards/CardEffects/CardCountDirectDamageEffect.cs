using UnityEngine;

[System.Serializable]
public class CardCountDirectDamageEffect : CardEffect
{
    // ================================================================
    // Parameters
    // ================================================================
    [Tooltip("The amount the our handsize is multiplied by to calculate damage.")]
    public int multiplier = 1;
    [Tooltip("The cardPile we count in order to determine damage.")]
    public CardPile cardPile;
    
    // ================================================================
    // Constructor
    // ================================================================
    public CardCountDirectDamageEffect() { Debug_ID = "New CardCountDirectDamage Effect"; }

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

        if (target.gameObject.TryGetComponent<Damagable>(out var damagable))
        {
            int amount = 0;
            if (cardPile == CardPile.drawPile) { amount = caller.drawPile.Count; }
            else if (cardPile == CardPile.hand) { amount = caller.hand.Count; } // Don't count ourselves
            else if (cardPile == CardPile.discardPile) { amount = caller.discardPile.Count; }
            else 
            { 
                Debug.LogError("CardCountDirectDamageEffect Error. Activate() failed. cardPile must not be NULL.", caller);
                EndEffect(card);
                return; 
            }

            Debug.Log($"did {amount * multiplier} damage");
            damagable.damage(amount * multiplier);
        }
        else
        {
            Debug.Log("The targeted entity is invincible!", caller);
        }
        
        EndEffect(card);
    }
}