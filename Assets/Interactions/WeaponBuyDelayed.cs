using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuyDelayed : WeaponBuy
{
    public bool working;
    public bool finished;
    [SerializeField] private float processDelay;
    [SerializeField] private float removalDelay;

    protected override void BuyThing()
    {
        if (Player.Instance.PlayerVariables.isDowned)
            return;
        //check if we have enough points && arent already working
        //if we do remove points and do the thing
        if (CanBuy() && !working)
        {
            DoThing();
            Player.Instance.PointManager.RemovePoints(cost);
        }
    }

    protected override void DoThing()
    {
        if (working)
        {
            return;
        }
        else if (finished)
        {
            PickUp();
        }
        else
        {
            InitialInteraction();
        }
    }

    protected virtual void InitialInteraction()
    {
        working = true;
        finished = false;

        interaction.HideText();

        Invoke(nameof(FinishProcess), processDelay);
    }

    protected virtual void FinishProcess()
    {
        finished = true;
        working = false;

        SetInteractionText(); // set text to finished text
        interaction.ShowText();

        Invoke(nameof(RemoveWeapon), removalDelay);
    }

    protected virtual void PickUp()
    {
        finished = false;
        working = false;

        SetInteractionText(); // set text to starting initial text
        interaction.ShowText();

        CancelInvoke(nameof(RemoveWeapon));
    }

    protected virtual void RemoveWeapon() //times up player lost change to pick up
    {
        finished = false;
        working = false;

        SetInteractionText(); // set text to starting initial text
        interaction.ShowText();
    }


}
