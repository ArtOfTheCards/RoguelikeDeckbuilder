using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Transform pc;
    Transform sprite;
    public bool inuse = false;

    void Awake() {
        pc = GameObject.Find("Player").transform;
        Transform[] children = GetComponentsInChildren<Transform>();
        sprite = children[1];
        // explosion = children[2].GetComponent<ParticleSystem>();
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
        this.inuse = false;
        transform.position = target.position;
        sprite.gameObject.SetActive(false);
        doWhenComplete();
    }

}
