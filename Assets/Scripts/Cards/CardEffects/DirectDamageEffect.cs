using UnityEngine;

[System.Serializable]
public class DirectDamageEffect : CardEffect
{
    [SerializeField, Tooltip("The amount this effect damages by.")]
    public int amount;
    public DirectDamageEffect() { Debug_ID = "New DirectDamage Effect"; }


    
    public override void Activate(CardUser caller, Card card, Targetable target)
    {
        if (target.gameObject.TryGetComponent<Damagable>(out var damagable))
        {
            damagable.damage(amount);
        }
        else
        {
            Debug.Log("The targeted entity is invincible!", caller);
        }
        
        EndEffect(card);
    }
}