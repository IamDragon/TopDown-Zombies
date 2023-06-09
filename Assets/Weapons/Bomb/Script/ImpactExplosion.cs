using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactExplosion : Explosion
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Explode();
        Destroy(gameObject);
    }
}
