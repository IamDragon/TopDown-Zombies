using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
