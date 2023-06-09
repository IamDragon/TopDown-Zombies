using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackAPunch : WeaponBuyDelayed
{
    [Header("PaP")]
    [SerializeField] private UpgradeMapper upgradeMapper;
    [SerializeField] private SpriteRenderer upgradedWeaponSprite;
    private Gun upgradedGun;
    protected override void Start()
    {
        base.Start();
        upgradedWeaponSprite.enabled = false;
    }

    protected override void SetInteractionText()
    {
        if (finished)
            interaction.SetInteractionText(" pick up " + upgradedGun.name);
        else
            interaction.SetInteractionText(" Pack-A-Punch gun " + costText);
    }

    protected override void InitialInteraction()
    {
        base.InitialInteraction();
        Gun gunToUpgrade = weaponHandler.RemoveActiveGun();
        upgradedGun = upgradeMapper.GetUpgradedWeapon(gunToUpgrade);
        upgradedGun.IsUpgraded = true;
        interaction.TextVisible = false;
    }

    protected override void FinishProcess()
    {
        //show weapon
        base.FinishProcess();
        upgradedWeaponSprite.sprite = upgradedGun.Sprite;
        upgradedWeaponSprite.enabled = true;
        interaction.TextVisible = true;
    }

    protected override void PickUp()
    {
        base.PickUp();
        upgradedWeaponSprite.enabled = false;
        weaponHandler.ReceiveNewGun(upgradedGun);
    }

    protected override void RemoveWeapon()
    {
        base.RemoveWeapon();
        upgradedWeaponSprite.enabled = false;
    }
}
