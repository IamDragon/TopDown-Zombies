using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTextManager : TextManager
{
    [Header("Events")]
    [SerializeField] private StringEventSO onInteractionEnterEvent;
    [SerializeField] private EventSO onInteractionExiterEvent;

    protected override void Start()
    {
        base.Start();
        textElement.text = "";
    }
    private void OnEnable()
    {
        onInteractionEnterEvent.Action += ShowText;
        onInteractionExiterEvent.Action += HideText;
    }

    private void OnDisable()
    {
        onInteractionEnterEvent.Action -= ShowText;
        onInteractionExiterEvent.Action -= HideText;
    }

    private void ShowText(string text)
    {
        textElement.text = text;
    }

    private void HideText()
    {
        textElement.text = "";
    }
}
