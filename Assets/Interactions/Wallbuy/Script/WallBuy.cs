using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuy : WeaponBuy
{
    [SerializeField] private int ammoCost;
    [SerializeField] private Gun gun;

    protected override void BuyThing()
    {
        if (Player.Instance.PlayerVariables.isDowned)
            return;
        //check if we have enough points, don't have the perk and don't have to many perks
        //if we do remove points and get
        if (CanBuy() && !Player.Instance.WeaponHandler.HasGun(gun))
        {
            DoThing();
            Player.Instance.PointManager.RemovePoints(cost);
        }
        else if (CanBuyAmmo() && Player.Instance.WeaponHandler.HasGun(gun)) // doesn't check if already has full ammo
        {
            //Give ammo to player
            BuyAmmo();
            Player.Instance.PointManager.RemovePoints(ammoCost);
        }
        else
            CantPurchase();
    }

    //change to inherit weaponbuy and to follow the standar weaponhandler interaction
    protected override void DoThing()
    {
        base.DoThing();
        Player.Instance.WeaponHandler.ReceiveNewGun(gun);
        SetInteractionText();
        interaction.ShowText();
        //Set text to buy ammo text
        //Interaction.OnInteractionExit();
    }

    private void BuyAmmo()
    {
        Player.Instance.WeaponHandler.FillAmmoForGun(gun);
        SetInteractionText();
        interaction.ShowText();
    }

    private bool CanBuyAmmo()
    {
        return Player.Instance.PointManager.CanAfford(ammoCost);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.transform.parent.CompareTag(interaction.PlayerTag))
        {
            SetInteractionText();
            //interaction.ShowText();
        }
    }

    protected override void SetInteractionText()
    {
        if (Player.Instance.WeaponHandler.HasGun(gun))
        {
            interaction.SetInteractionText(" purchase ammo for " + gun.name + " " + costText);
        }
        else
        {
            interaction.SetInteractionText(" purchase " + gun.name + " " + costText);
        }
    }
}
