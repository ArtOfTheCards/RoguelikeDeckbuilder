using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] float DestroyDelay = 3f;
    [SerializeField] private Vector3 Offset = new Vector3(0, 2, 0);

    void Start()
    {
        Destroy(gameObject, DestroyDelay);

        transform.localPosition += Offset;
    }
}
