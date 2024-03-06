using System.Collections;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField, Tooltip("Whether or not this Damagable should have a healthbar created for it at runtime.\n\nDefault: true")] 
    bool createHealthbar = true;
    [SerializeField] private int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }
    [SerializeField] public int maxHealth;
    public int MaxHealth { get { return maxHealth; } }
    [SerializeField] public int debugHealthBoxMove;
    public System.Action<StatModifierBank> OnCalculateDamage;
    public System.Action deathTrigger;
    [SerializeField] private SpriteRenderer sprite;

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
        showDamageIndicator(finalValue);
    }

    void showDamageIndicator(int value) {
        if (indicatorPrefab == null) return;

        Transform canvasTransform = GameObject.FindGameObjectWithTag("IndicatorCanvas").transform;
        GameObject indicatorObj = Instantiate(indicatorPrefab, Vector3.zero, Quaternion.identity, canvasTransform);
        indicatorObj.GetComponent<DamageIndicator>().Initialize(value, transform.position);
    }

    public void heal(int value) {
        currentHealth = Mathf.Min(currentHealth + value, maxHealth);
    }

    private void die() {
        Destroy(this.gameObject);
        deathTrigger?.Invoke();
    }

    private IEnumerator DEBUG_FlashRed(SpriteRenderer sprite)
    {
        Color old = sprite.color;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.15f);
        sprite.color = old;
    }

}
