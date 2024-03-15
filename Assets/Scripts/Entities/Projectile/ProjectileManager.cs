using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using FMODUnity;

public class ProjectileManager : MonoBehaviour
{
    // how you use this class is up to you
    // do you want to instantiate projectiles in real time?
    // do you want to use a small pool of projectiles that are always ready to use?
    // set them all up here
    
    List<Projectile> children = new();
    List<Projectile> childrenSpecial = new();

    void Awake() {
        foreach(Projectile child in GetComponentsInChildren<Projectile>())
        {
            if(child.transform.name == "projectile")
            {
                children.Add(child);
            }
            else
            {
                childrenSpecial.Add(child);
            }
        }
        //children = GetComponentsInChildren<Projectile>(); // for testing, just grab the first (and only) projectile
        //childrenSpecial = GetComponentsInChildren<Projectile>();
        Debug.Log("total children: " + children.Count);
        Debug.Log("total special children: " + childrenSpecial.Count);

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
        for(int i = 0; i < children.Count-1; i++)
        {
            if(children[i].inuse == false) 
            {
                Debug.Log(children[i].inuse);
                Debug.Log("trigger throw next");
                children[i].inuse = true;
                Debug.Log(children[i].inuse);
                StartCoroutine(children[i].throwAt(target.transform, () => {

                    Debug.Log("do damage based on card: " + card);
                    DoDamage(target, card);
                    DoStatus(target, card);
                    PlaySFXByID(card);

                }));
                break;
            }
        }
        
    }
    public void throwNextSpecial(Transform thrower, Damagable target, Card card, String projectileName) {
        // TODO: when there is more than one child, we need to figure out which one is "next"
        for(int i = 0; i < childrenSpecial.Count-1; i++)
        {
            if(childrenSpecial[i].transform.name == projectileName)
            {
                if(childrenSpecial[i].inuse == false) 
                {
                    //Debug.Log(childrenSpecial[i].inuse);
                    //Debug.Log("trigger throw next");
                    childrenSpecial[i].inuse = true;
                    //Debug.Log(childrenSpecial[i].inuse);
                    StartCoroutine(childrenSpecial[i].specialThrowAt(thrower, target.transform, () => {

                        //Debug.Log("do damage based on card: " + card);
                        DoDamage(target, card);
                        DoStatus(target, card);
                        // trigger sfx
                        PlaySFXByID(card);
                    }));
                    break;
                }
            }
            
        }
        
    }

    private void DoDamage(Damagable target, Card card)
    {
        foreach (CardEffect teffect in card.throwEffects)
        {
            if (teffect.GetType().FullName == "DirectDamageEffect")
            {
                DirectDamageEffect dDE = (DirectDamageEffect)teffect;

                target.damage(dDE.amount);
            }
            if (teffect.GetType().FullName == "CardCountDirectDamageEffect")
            {
                CardCountDirectDamageEffect ccdDE = (CardCountDirectDamageEffect)teffect;
                CardUser cardUser = FindObjectOfType<CardUser>();

                int amount = 0;
                if (ccdDE.cardPile == CardPile.drawPile) { amount = cardUser.drawPile.Count; }
                else if (ccdDE.cardPile == CardPile.hand) { amount = cardUser.hand.Count; } // Don't count ourselves
                else if (ccdDE.cardPile == CardPile.discardPile) { amount = cardUser.discardPile.Count; }

                target.damage(amount);
            }
            if (teffect.GetType().FullName == "SpawnEffect")
            {
                SpawnEffect sE = (SpawnEffect)teffect;

                //Debug.Log(sE.toSpawn + "spawn");
                GameObject.Instantiate(sE.toSpawn, target.transform.position, Quaternion.identity);
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
                //Debug.Log(dASE.status.ID + " applied (hypothetically)");

                if (target.TryGetComponent<Effectable>(out Effectable effectable))
                {
                    effectable.AddStatusEffect(dASE.status);
                }
            }


        }
    }

     private void PlaySFXByID(Card card) {

        string propertyName = card.debug_ID;

        PropertyInfo propertyInfo = typeof(FMODEvents).GetProperty(propertyName);

        if (propertyInfo != null)
        {
            CardSFX cardSFX = (CardSFX)propertyInfo.GetValue(FMODEvents.instance);
            EventReference eventRef = cardSFX.Throw;

            // if (eventRef.Path.Length == 0) {
            //     // Debug.LogError("SFX Fail: Property '" + propertyName + "' does not have a valid THROW Event Reference");
            // } else {
            //     AudioManager.instance.PlayOneShot(eventRef, new Vector3(0, 0, 0));
            // }
            AudioManager.instance.PlayOneShot(eventRef, new Vector3(0, 0, 0));

        }
        else
        {
            Debug.LogError("SFX Fail: Property '" + propertyName + "' not found in FMODEvents");
        }
    }




}
