using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;



public class CrabAttack : MonoBehaviour
{

    [SerializeField]
    public Card crabAttack;
    //[SerializeField]
    //private float tickLength = 1.5f; 
    [SerializeField]
    private TargetAffiliation[] targets;
    private Dictionary<Damagable, float> damagableToTickTime = new();
    
    [SerializeField]
    private GameObject projectileManager;

    [SerializeField]
    private GameObject bubble;

    private Transform crabLocation;

    //public List<Transform> crabList = new();

    void Start()
    {
        projectileManager = GameObject.Find("projectilePool");
        bubble = GameObject.Find("bubble");
        crabLocation = this.transform.parent;
        //crabList.Add(crabLocation);
        //projectileManager = (ProjectileManager); 
        //Debug.Log(projectileManager.name);
        foreach(GameObject ally in GameObject.FindGameObjectsWithTag("Ally"))
        {
            if(ally.name == "Crab_Agent(Clone)")
            {
                crabThrow(ally.transform);
            }
            //crabThrow(crab);
        }
    }
    private void Update()
    {
        /*if (damagableToTickTime.Keys.Count != 0)
        {
            // We use ToArray so that if our dictionary is modified while looping, we don't get any errors.
            foreach (Damagable damagable in damagableToTickTime.Keys.ToArray())
            {
                // In case our dictionary changes while looping and our array is no longer accurate.
                if (damagableToTickTime.ContainsKey(damagable) && damagable != null) 
                {
                    if (damagableToTickTime[damagable] >= tickLength)
                    {
                        projectileManager.GetComponent<ProjectileManager>().throwNextSpecial(crabLocation, damagable, crabAttack);
                        //damagable.damage(damagePerTick);
                        damagableToTickTime[damagable] = 0;
                    }

                    damagableToTickTime[damagable] += Time.deltaTime;
                }
            }
        }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Targetable targetable = other.gameObject.GetComponentInChildren<Targetable>();
        if (targetable != null)
        {
            if (targets.Contains(targetable.affiliation)) // Only add targetables we can target.
            {
                Damagable damagable = other.gameObject.GetComponentInChildren<Damagable>();
                if (damagable != null) 
                {
                    //damagable.damage(damagePerTick);
                    projectileManager.GetComponent<ProjectileManager>().throwNextSpecial(crabLocation, damagable, crabAttack);
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

    void crabThrow(Transform crab)
    {
        Targetable targetable = GetComponentInChildren<Targetable>();
            if (targetable != null)
            {
                if (targets.Contains(targetable.affiliation)) // Only add targetables we can target.
                {
                    Damagable damagable = GetComponentInChildren<Damagable>();
                    if (damagable != null) 
                    {
                        //damagable.damage(damagePerTick);
                        projectileManager.GetComponent<ProjectileManager>().throwNextSpecial(crab, damagable, crabAttack);
                    }   
                }
            }
    }

    // Start is called before the first frame update
    
}
