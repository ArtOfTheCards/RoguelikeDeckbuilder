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
            // TODO: put damage logic here
        }));
    }

}
