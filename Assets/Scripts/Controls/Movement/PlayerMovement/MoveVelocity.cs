using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocity : MonoBehaviour, IMoveVelocity
{

    private Vector3 velocityVector;
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private Rigidbody2D rb;

    public void SetVelocity(Vector3 velocityVector)
    {
        this.velocityVector = velocityVector;
    }

    private void FixedUpdate()
    {
        rb.velocity = velocityVector * moveSpeed;
    }


}
