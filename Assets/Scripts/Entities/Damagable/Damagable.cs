using System.Collections;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private GameObject damageIndicator;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    public System.Action<StatModifierBank> OnCalculateDamage;

    public void damage(int baseValue) 
    {
        // Create a new bank of modifiers to our damage amount.
        StatModifierBank damageModifiers = new();                          
        // Populate that bank with the modifiers that our subscribees provide us.
        OnCalculateDamage?.Invoke(damageModifiers);                        
        // Calculate the amount of damage done.
        int finalValue = (int)damageModifiers.Calculate(baseValue);
        Debug.Log($"Base damage was {baseValue}. With modifiers, did {finalValue} damage!");

        
        currentHealth = Mathf.Max(currentHealth - finalValue, 0);

        // DEBUG CODE. DEBUG CODE. DEBUG CODE.
        // DEBUG CODE. DEBUG CODE. DEBUG CODE.
        if (TryGetComponent<SpriteRenderer>(out var sprite)) StartCoroutine(DEBUG_FlashRed(sprite));
        // DEBUG CODE. DEBUG CODE. DEBUG CODE.
        // DEBUG CODE. DEBUG CODE. DEBUG CODE.

        if (currentHealth == 0) {
            die();
        }

        // show damage indicator
        showDamageIndicator(value);
    }

    void showDamageIndicator(int value) {
        if (damageIndicator == null) return;

        var text = Instantiate(damageIndicator, transform.position, Quaternion.identity, transform);
        text.GetComponent<TextMesh>().text = value.ToString();
    }

    public void heal(int value) {
        currentHealth = Mathf.Min(currentHealth + value, maxHealth);
    }

    private void die() {
        Destroy(this.gameObject);
    }

    private IEnumerator DEBUG_FlashRed(SpriteRenderer sprite)
    {
        Color old = sprite.color;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        sprite.color = old;
    }
}
