using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    protected Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetVelocity(Vector3 direction, float speed)
    {
        rb.velocity = direction * speed;
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    public void SetRBVel0()
    {
        rb.velocity = Vector3.zero;
    }
}
