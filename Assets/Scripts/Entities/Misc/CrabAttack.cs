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

    //public Damagable targetLock;
    //public System.Action onCrab;

    //public List<Transform> crabList = new();

    void Start()
    {
        projectileManager = GameObject.Find("projectilePool");
        bubble = GameObject.Find("bubble");
        crabLocation = this.transform.parent;
        //crabList.Add(crabLocation);
        //projectileManager = (ProjectileManager); 
        //Debug.Log(projectileManager.name);
        /*foreach(Crab crab in FindObjectsOfType<Crab>())
        {
            Debug.Log("CRAB THROWING" + crab.transform.position + "AT THE" + crab.targetLock);
            crabThrow(crab.transform, crab.targetLock);
            //crabThrow(crab);
        }*/
        //onCrab?.Invoke();
        Damagable damagable = this.transform.parent.gameObject.GetComponent<Damagable>();
        damagable.onCrab += CrabEnter;
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
    void CrabEnter()
    {
        Debug.Log("ON CRAB");
        Crab crab = this.transform.gameObject.GetComponent<Crab>();
        crabThrow(this.crabLocation, crab.targetLock);
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
                    Crab crab = this.transform.parent.gameObject.GetComponent<Crab>();
                    crab.targetLock = damagable;
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

    public void crabThrow(Transform crab, Damagable target)
    {
        if (target != null)
        {
            projectileManager.GetComponent<ProjectileManager>().throwNextSpecial(crab, target, crabAttack);
        }
    }

    // Start is called before the first frame update
    
}
