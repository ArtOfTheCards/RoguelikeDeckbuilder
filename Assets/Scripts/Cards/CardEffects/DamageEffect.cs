using UnityEngine;

[System.Serializable]
public class DamageEffect : CardEffect
{
    [SerializeField, Tooltip("The amount this effect damages by.")]
    public int value;
    public DamageEffect() { Debug_ID = "New Damage Effect"; }
    public override void Activate<T>(CardUser caller, Card card, T target)
    {
        if (target is Targetable)
        {
            Targetable targetable = target as Targetable;

            if (targetable.gameObject.TryGetComponent<Damagable>(out var damagable))
            {
                damagable.damage(value);
            }
            else
            {
                Debug.Log("The targeted entity is invincible!", caller);
            }
        }
        else // target is Vector3
        {
            // Worldspace targeting not supported yet.
        }

        EndEffect(card);
    }
}