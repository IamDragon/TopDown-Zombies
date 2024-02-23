using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkBuy : Buy
{
    //events 
    [SerializeField] protected EventSO onInteractionExitEvent;

    [Header("Perk")]
    [SerializeField] private Perk perk;

    protected override void BuyThing()
    {
        if (Player.Instance.PlayerVariables.isDowned)
            return;
        //check if we have enough points, dont have the perk and dont have ot many perks
        //if we do remove points and ge
        if (Player.Instance.PointManager != null)
        {
            if (Player.Instance.PointManager.CanAfford(cost) && Player.Instance.PerkHandler.CanPurchasePerk(perk))
            {
                DoThing();
                Player.Instance.PointManager.RemovePoints(cost);
                onInteractionExitEvent.Invoke();
            }
            else if(!Player.Instance.PointManager.CanAfford(cost) && !Player.Instance.PerkHandler.CanPurchasePerk(perk))
                onCantAffordEvent.Invoke();
        }
    }

    protected override void DoThing()
    {
        base.DoThing();
        Debug.Log(this + " bought");
        Player.Instance.PerkHandler.RecievePerk(perk);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //base.OnTriggerEnter2D(collision);
        if (collision.transform.parent && collision.transform.parent.CompareTag(interaction.PlayerTag)
            && !Player.Instance.PlayerVariables.isDowned)
        {
            onInteractionTriggerdEvent.Action += BuyThing;

            if (Player.Instance.PerkHandler.CanPurchasePerk(perk))
                interaction.ShowText();
            else
                interaction.TextVisible = false;
        }
    }

    protected override void SetInteractionText()
    {
        interaction.SetInteractionText(" purchase " + perk.PerkName + " " + costText);
    }
}
