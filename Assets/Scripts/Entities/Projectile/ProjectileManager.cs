using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // how you use this class is up to you
    // do you want to instantiate projectiles in real time?
    // do you want to use a small pool of projectiles that are always ready to use?
    // set them all up here
    
    Projectile[] children;

    void Awake() {
        children = GetComponentsInChildren<Projectile>(); // for testing, just grab the first (and only) projectile
        Debug.Log("total children: " + children.Length);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void throwNext(Damagable target, Card card) {
        // TODO: when there is more than one child, we need to figure out which one is "next"
        Debug.Log("trigger throw next");
        StartCoroutine(children[0].throwAt(target.transform, () => {
            Debug.Log("do damage based on card: " + card);
            
            DoDamage(target, card);
            DoStatus(target, card);
            
        }));
    }

    private void DoDamage(Damagable target, Card card)
    {
        foreach (CardEffect teffect in card.throwEffects)
        {
            if (teffect.GetType().FullName == "DirectDamageEffect")
            {
                DirectDamageEffect dDE = (DirectDamageEffect)teffect;

                Debug.Log(dDE.amount + " hypothetical damage");
                target.damage(dDE.amount);
            }
        }
    }
    private void DoStatus(Damagable target, Card card)
    {
        foreach (CardEffect seffect in card.throwEffects)
        {
            if (seffect.GetType().FullName == "DirectAddStatusEffect")
            {
                DirectAddStatusEffect dASE = (DirectAddStatusEffect)seffect;
                Debug.Log(dASE.status.ID + " applied (hypothetically)");

                if (target.TryGetComponent<Effectable>(out Effectable effectable))
                {
                    effectable.AddStatusEffect(dASE.status);
                }
            }


        }
    }




}
