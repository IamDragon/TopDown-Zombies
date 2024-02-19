using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeIconManager : IconManager
{

    [Header("Events")]
    [SerializeField] private SpriteEventSO onGrenadeFillEvent;
    [SerializeField] private SpriteEventSO onGrenadeInecreaseEvent;
    [SerializeField] private EventSO onGrenadeDecreaseEvent;

    /*
     * start show all icons either here or grenadeManager send out action
     * start grenadeMAnager send out action for each grenade and an icon isGreated
     * onFill -> ShowIcons
     * OnGrenadeDecrease -> HideIcon
    */
    private void OnEnable()
    {
        onGrenadeFillEvent.Action += SetAndShowIcons;
        onGrenadeInecreaseEvent.Action += ShowIcon;
        onGrenadeDecreaseEvent.Action += HideLastIcon;
    }

    private void OnDisable()
    {
        onGrenadeFillEvent.Action -= SetAndShowIcons;
        onGrenadeInecreaseEvent.Action -= ShowIcon;
        onGrenadeDecreaseEvent.Action -= HideLastIcon;
    }
}
