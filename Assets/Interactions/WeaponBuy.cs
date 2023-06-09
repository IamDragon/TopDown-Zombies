using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuy : Buy
{
    public WeaponHandler weaponHandler;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Debug.Log(this + " trigger");
        if (collision.transform.parent.CompareTag(interaction.PlayerTag))
        {
            weaponHandler = collision.transform.parent.GetComponentInChildren<WeaponHandler>();
        }
    }
}
