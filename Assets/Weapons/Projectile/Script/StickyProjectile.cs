using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyProjectile : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent = collision.transform;
        base.OnTriggerEnter2D(collision);
    }
}
