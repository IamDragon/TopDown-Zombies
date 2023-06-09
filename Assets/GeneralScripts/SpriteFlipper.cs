using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        FlipSrite();
    }

    private void FlipSrite()
    {
        if (rb.velocity.x != 0)
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }
}
