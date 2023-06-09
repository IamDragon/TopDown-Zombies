using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Perks/MuleKick")]
public class MuleKick : Perk
{
    private WeaponHandler weaponHandler;

    public override void ActivatePerk()
    {
        base.ActivatePerk();
        weaponHandler.CanHoldExtraGun = true;
    }

    public override void DeactivatePerk()
    {
        base.DeactivatePerk();
        weaponHandler.CanHoldExtraGun = false;
    }

    public override void GetComponentHandler(GameObject player)
    {
        base.GetComponentHandler(player);
        weaponHandler = player.GetComponentInChildren<WeaponHandler>();
    }
}
