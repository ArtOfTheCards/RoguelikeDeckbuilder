using System.Collections;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] private GameObject indicatorPrefab;
    [SerializeField, Tooltip("Whether or not this Damagable should have a healthbar created for it at runtime.\n\nDefault: true")] 
    bool createHealthbar = true;
    [SerializeField] public int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }  // read-only property
    [SerializeField] public int maxHealth;
    public int MaxHealth { get { return maxHealth; } }  // read-only property
    public System.Action<StatModifierBank> OnCalculateDamage;
    [SerializeField] private SpriteRenderer sprite;

    public System.Action deathTrigger;

    public System.Action onCrab;

    private Transform worldspaceCanvasTransform = null;
    private WorldspaceHealthbars worldspaceHealthbars;

    private void Awake()
    {
        // Sets our current health, and gets our needed canvas UI references.
        // ================

        currentHealth = maxHealth;

        worldspaceCanvasTransform = GameObject.FindGameObjectWithTag("WorldspaceIndicators").transform;
        if (worldspaceCanvasTransform == null)
        {
            Debug.LogError("Damagable error: Awake failed. The scene has no indicator canvas, or the indicator canvas is not tagged as \"IndicatorCanvas\"");
        }
        else 
        {
            worldspaceHealthbars = worldspaceCanvasTransform.GetComponentInChildren<WorldspaceHealthbars>();
        }
    }
    
    private void Start()
    {
        if (worldspaceHealthbars != null && createHealthbar)
        {
            Debug.Log("Created healthbar");
            worldspaceHealthbars.CreateHealthbar(this);
        }
        if(this.transform.name == "Crab_Agent(Clone)")
        {
            onCrab?.Invoke();
        }
    }

    private void OnDestroy()
    {
        if (worldspaceHealthbars != null && createHealthbar)
        {
            worldspaceHealthbars.DeleteHealthbar(this);
        }
    }

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
        if (sprite != null) StartCoroutine(DEBUG_FlashRed(sprite));
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

        GameObject indicatorObj = Instantiate(indicatorPrefab, Vector3.zero, Quaternion.identity, worldspaceCanvasTransform);
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
