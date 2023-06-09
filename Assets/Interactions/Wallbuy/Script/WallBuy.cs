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
        //check if we have enough points, dont have the perk and dont have ot many perks
        //if we do remove points and ge
        if (CanBuy() && !Player.Instance.WeaponHandler.HasGun(gun))
        {
            DoThing();
            Player.Instance.PointManager.RemovePoints(cost);
        }
        else if (CanBuyAmmo() && Player.Instance.WeaponHandler.HasGun(gun)) // doesnt already have full ammo
        {
            //Give ammo to player
            BuyAmmo();
            Player.Instance.PointManager.RemovePoints(ammoCost);
        }
        else
            onCantAffordEvent.Invoke();
    }

    //change to inherit weaponbuy and to follow the standar weaponhandler interaction
    protected override void DoThing()
    {
        Player.Instance.WeaponHandler.ReceiveNewGun(gun);
        //Set text to buy ammo text
        //Interaction.OnInteractionExit();
    }

    private void BuyAmmo()
    {
        Player.Instance.WeaponHandler.FillAmmoForGun(gun);
        SetInteractionText();
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
            interaction.ShowText();
        }
    }

    protected override void SetInteractionText()
    {
        if (Player.Instance.WeaponHandler.HasGun(gun))
            interaction.SetInteractionText(" purchase ammo for " + gun.name + " " + costText);
        else
            interaction.SetInteractionText(" purchase " + gun.name + " " + costText);
    }
}
