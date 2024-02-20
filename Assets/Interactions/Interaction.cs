using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Interaction : MonoBehaviour
{
    [SerializeField] private string playerTag;
    private bool playerInRange;

    //HUD text
    public string interactionText;
    //private string baseInteractionText = "Press " + Keybinds.Instance.InteractionButton.ToString() + " to";
    private string baseInteractionText = "Press E" + " to";

    //Actions & events
    [Header("Events")]
    [SerializeField] protected EventSO onUse;
    [SerializeField] private EventSO onInteractionTriggerdEvent;
    [SerializeField] private StringEventSO onInteractionEnterEvent;
    [SerializeField] private EventSO onInteractionExitEvent;

    //Properties
    public string PlayerTag { get { return playerTag; } }
    public bool TextVisible { get; set; } = true;
    public bool PlayerInRange { get { return playerInRange; } }

    private void Start()
    {
        //baseInteractionText = "Press " + Keybinds.Instance.InteractionButton.ToString() + " to";
    }

    private void TriggerInteraction()
    {
        Debug.Log("Interaction triggered");
        onInteractionTriggerdEvent.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            playerInRange = true;
            onUse.Action += TriggerInteraction;
            if (TextVisible)
                onInteractionEnterEvent.Invoke(interactionText);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            playerInRange = false;
            onUse.Action -= TriggerInteraction;
            onInteractionExitEvent.Invoke();
        }
    }

    public void SetInteractionText(string text)
    {
        interactionText = baseInteractionText + text;
    }

    public void SetAndShowText(string text)
    {
        SetInteractionText(text); 
        ShowText();
    }
    public void ShowText()
    {
        TextVisible = true;
        if (PlayerInRange)
            onInteractionEnterEvent.Invoke(interactionText);
    }

    public void HideText()
    {
        TextVisible = false;
        if (PlayerInRange)
            onInteractionExitEvent.Invoke();
    }
}
