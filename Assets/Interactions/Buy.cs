using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Buy : MonoBehaviour
{
    [Header("Cost")]
    [SerializeField] protected int cost;

    [Header("Audio")]
    [SerializeField] protected AudioClip purchaseAcceptedClip;
    [SerializeField] protected AudioClip purchaseDeniedClip;
    protected AudioSource audioSource;

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
        //interactionText = "Press " + Keybinds.Instance.InteractionButton.ToString() + " to";
        interactionText = "Press E" + " to";
        interaction = GetComponent<Interaction>();
        SetInteractionText();
        audioSource = GetComponent<AudioSource>();

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

    }
    /// <summary>
    /// Meant to call event to notify receiver that purchase was made
    /// </summary>
    protected virtual void DoThing()
    {
        audioSource.PlayOneShot(purchaseAcceptedClip);
    }

    protected virtual void CantPurchase()
    {
        onCantAffordEvent.Invoke();
        audioSource.PlayOneShot(purchaseDeniedClip);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent && collision.transform.parent.CompareTag(interaction.PlayerTag)
            && !Player.Instance.PlayerVariables.isDowned)
        {
            onInteractionTriggerdEvent.Action += BuyThing;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent && collision.transform.parent.CompareTag(interaction.PlayerTag)
            && !Player.Instance.PlayerVariables.isDowned)
        {
            onInteractionTriggerdEvent.Action -= BuyThing;
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
