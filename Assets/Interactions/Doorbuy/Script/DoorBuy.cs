using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBuy : Buy
{
    [SerializeField] protected EventSO onMapUpdate;
    [Header("Door object")]
    [SerializeField] private GameObject door;
    protected override void DoThing()
    {
        door.SetActive(false);
        onMapUpdate.Invoke();
    }

    protected override void SetInteractionText()
    {
        interaction.SetInteractionText(" open door " + costText);
    }
}
