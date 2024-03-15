using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;

[RequireComponent(typeof(Collider2D))]
public class ButterflyAttack : MonoBehaviour
{
    public enum TickType { Once, Continuous }

    [SerializeField, Tooltip("The amount of damage dealt per tick.\n\nDefault: 1")]
    private int damagePerTick = 1;
    [SerializeField, Tooltip("Whether we should damage a target once, or continuously.\n\nDefault: Once")]
    private TickType tickType = TickType.Continuous;
    [ShowIf("tickType", TickType.Continuous), SerializeField, Tooltip("The length of time, in seconds, between ticks.\n\nDefault: 1.5")]
    private float tickLength = 1.5f;
    [SerializeField, Tooltip("Whether we should target all targetables, regardless of TargetAffiliation.\n\nDefault: false")]
    private bool targetEverything = false;
    [HideIf("targetEverything"), SerializeField, Tooltip("The TargetAffiliations we should damage.")]
    private TargetAffiliation[] targets;


    private Dictionary<Damagable, float> damagableToTickTime = new();

    [SerializeField]
    public Card butterflyAttack;
    
    [SerializeField]
    private GameObject projectileManager;

    [SerializeField]
    private GameObject laser;

    private Transform butterflyLocation;

    void Start()
    {
        projectileManager = GameObject.Find("projectilePool");
        laser = GameObject.Find("laser");
        butterflyLocation = this.transform.parent;
    }

    private void Update()
    {
        if (damagableToTickTime.Keys.Count != 0)
        {
            // We use ToArray so that if our dictionary is modified while looping, we don't get any errors.
            foreach (Damagable damagable in damagableToTickTime.Keys.ToArray())
            {
                // In case our dictionary changes while looping and our array is no longer accurate.
                if (damagableToTickTime.ContainsKey(damagable) && damagable != null) 
                {
                    if (damagableToTickTime[damagable] >= tickLength)
                    {
                        projectileManager.GetComponent<ProjectileManager>().throwNextSpecial(butterflyLocation, damagable, butterflyAttack, "laser");
                        damagableToTickTime[damagable] = 0;
                    }

                    damagableToTickTime[damagable] += Time.deltaTime;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Targetable targetable = other.gameObject.GetComponentInChildren<Targetable>();
        if (targetable != null)
        {
            if (targetEverything || targets.Contains(targetable.affiliation)) // Only add targetables we can target.
            {
                Damagable damagable = other.gameObject.GetComponentInChildren<Damagable>();
                if (damagable != null) 
                {
                    if (tickType == TickType.Once) 
                    {
                        damagable.damage(damagePerTick);
                    }

                    if (tickType == TickType.Continuous) 
                    {
                        damagableToTickTime[damagable] = tickLength;
                    }
                }   
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Damagable damagable = other.gameObject.GetComponentInChildren<Damagable>();
        if (damagable != null && damagableToTickTime.ContainsKey(damagable)) 
        {
            damagableToTickTime.Remove(damagable);
        }
    }
}