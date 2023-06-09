using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Perks/DoubleTap")]
public class DoubleTap : Perk
{
    private WeaponHandler weaponHandler;
    public override void ActivatePerk()
    {
        base.ActivatePerk();
        weaponHandler.ActivateExtraShot();
    }

    public override void DeactivatePerk()
    {
        base.DeactivatePerk();
        weaponHandler.DeactivateExtraShot();
    }

    public override void GetComponentHandler(GameObject player)
    {
        weaponHandler = player.GetComponentInChildren<WeaponHandler>();
    }
}
