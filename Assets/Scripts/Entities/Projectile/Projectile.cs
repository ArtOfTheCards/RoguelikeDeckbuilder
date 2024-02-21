using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Transform pc;
    Transform sprite;

    void Awake() {
        pc = GameObject.Find("Player").transform;
        Transform[] children = GetComponentsInChildren<Transform>();
        sprite = children[1];
        // explosion = children[2].GetComponent<ParticleSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator throwAt(Transform target, Action doWhenComplete) {
        sprite.gameObject.SetActive(true);
        transform.position = pc.transform.position;

        float throwSpeed = 5;

        while (Vector3.Distance(transform.position, target.position) > 1.5f)
        {
            // Calculate the direction towards the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Move towards the target
            transform.position += direction * throwSpeed * Time.deltaTime;

            float d = Vector3.Distance(transform.position, target.position);

            // Wait for the next frame
            yield return null;
        }


        Debug.Log("EY completed throw");
        transform.position = target.position;
        sprite.gameObject.SetActive(false);
        Debug.Log("do some custom damage based on card");
        doWhenComplete();
    }

}
