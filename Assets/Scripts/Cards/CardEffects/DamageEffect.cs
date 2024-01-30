using UnityEngine;

[System.Serializable]
public class DamageEffect : CardEffect
{
    [SerializeField, Tooltip("The amount this effect damages by.")]
    public int value;
    public DamageEffect() { Debug_ID = "New Damage Effect"; }


    
    public override void Activate(CardUser caller, Card card, Targetable target)
    {
        if (target.gameObject.TryGetComponent<Damagable>(out var damagable))
        {
            damagable.damage(value);
        }
        else
        {
            Debug.Log("The targeted entity is invincible!", caller);
        }
        
        EndEffect(card);
    }

    public override void Activate(CardUser caller, Card card, Vector3 target)
    {
        Debug.Log("DamageEffect does not currently have an implementation with a Vector3 target!", caller);
        
        EndEffect(card);
    }
}