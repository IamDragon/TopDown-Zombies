using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Perks/SpeedCola")]
public class SpeedCola : Perk
{
    [SerializeField] private float reloadSpeedFactor;
    private WeaponHandler weaponHandler;
    public override void ActivatePerk()
    {
        base.ActivatePerk();
        if(weaponHandler != null )
            weaponHandler.ReloadSpeedMultiplier = reloadSpeedFactor;
        else
            Debug.LogWarning("WeaponHandler was null");
    }

    public override void DeactivatePerk()
    {
        base.DeactivatePerk();
        weaponHandler.ResetReloadSpeedMultiplier();
    }

    public override void GetComponentHandler(GameObject player)
    {
        weaponHandler = player.GetComponentInChildren<WeaponHandler>();
    }
}
