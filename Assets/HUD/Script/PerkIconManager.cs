using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PerkIconManager : IconManager
{

    [Header("Events")]
    [SerializeField] private SpriteEventSO OnActivatePerkEvent;
    [SerializeField] private EventSO OnPlayerDownedEvent;

    private void OnEnable()
    {
        OnActivatePerkEvent.Action += ShowIcon;
        OnPlayerDownedEvent.Action += HideIcons;
    }

    private void OnDisable()
    {
        OnActivatePerkEvent.Action -= ShowIcon;
        OnPlayerDownedEvent.Action -= HideIcons;
    }
}
