using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageOnContact : MonoBehaviour
{
    [SerializeField, Tooltip("The amount of damage dealt per tick.\n\nDefault: 1")]
    private int damagePerTick = 1;
    [SerializeField, Tooltip("The length of time, in seconds, between ticks.\n\nDefault: 1.5")]
    private float tickLength = 1.5f;

    private Dictionary<Damagable, float> damagableToTickTime = new();


    private void Update()
    {
        if (damagableToTickTime.Keys.Count != 0)
        {
            // We use ToArray so that if our dictionary is modified while looping, we don't get any errors.
            foreach (Damagable damagable in damagableToTickTime.Keys.ToArray())
            {
                // In case our dictionary changes while looping and our array is no longer accurate.
                if (damagableToTickTime.ContainsKey(damagable)) 
                {
                    if (damagableToTickTime[damagable] >= tickLength)
                    {
                        damagable.damage(damagePerTick);
                        damagableToTickTime[damagable] = 0;
                    }

                    damagableToTickTime[damagable] += Time.deltaTime;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Damagable damagable = other.gameObject.GetComponentInChildren<Damagable>();
        if (damagable != null) 
        {
            damagableToTickTime[damagable] = tickLength;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Damagable damagable = other.gameObject.GetComponentInChildren<Damagable>();
        if (damagable != null) 
        {
            damagableToTickTime.Remove(damagable);
        }
    }
}