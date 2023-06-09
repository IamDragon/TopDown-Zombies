using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Buy : MonoBehaviour
{
    [Header("Cost")]
    [SerializeField] protected int cost;


    [Header("Event")]
    [SerializeField] protected EventSO onInteractionTriggerdEvent;
    [SerializeField] protected EventSO onCantAffordEvent;

    protected PointManager pointManager;
    protected Interaction interaction;
    protected string costText;
    protected string interactionText;

    protected virtual void Start()
    {
        costText = "[ Cost: " + cost + " ]";
        interactionText = "Press " + Keybinds.InteractionButton.ToString() + " to";
        interaction = GetComponent<Interaction>();
        SetInteractionText();

    }

    private void OnEnable()
    {
        //onInteractionTriggerdEvent.Action += BuyThing;

    }

    private void OnDisable()
    {
    }

    protected virtual void BuyThing()
    {
        if (Player.Instance.PlayerVariables.isDowned)
            return;
        //check if we have enough points
        //if we do remove points and do the thing
        if (CanBuy())
        {
            DoThing();
            Player.Instance.PointManager.RemovePoints(cost);
        }
        else
            onCantAffordEvent.Invoke();

    }
    /// <summary>
    /// Meant to call event to notify receiver that purchase was made
    /// </summary>
    protected virtual void DoThing()
    {

    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent && collision.transform.parent.CompareTag(interaction.PlayerTag)
            && !Player.Instance.PlayerVariables.isDowned)
        {
            onInteractionTriggerdEvent.Action += BuyThing;
            //interaction.ShowText();
            //interaction.TextIsViSable = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent && collision.transform.parent.CompareTag(interaction.PlayerTag)
            && !Player.Instance.PlayerVariables.isDowned)
        {
            onInteractionTriggerdEvent.Action -= BuyThing;
            //interaction.ShowText();
            //interaction.TextIsViSable = true;
        }
    }

    protected virtual void SetInteractionText()
    {
    }

    protected bool CanBuy()
    {
        return Player.Instance.PointManager.CanAfford(cost);
    }
}
