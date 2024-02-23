using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBuy : Buy
{
    [SerializeField] protected EventSO onMapUpdate;
    [Header("Door object")]
    [SerializeField] private GameObject door;
    private DoorOpen doorOpen;

    protected override void Start()
    {
        base.Start();
        doorOpen = GetComponent<DoorOpen>();
    }


    protected override void DoThing()
    {
        base.DoThing();
        doorOpen.OpenDoor();
        onMapUpdate.Invoke();
        interaction.TextVisible = false;
        interaction.HideText();
    }

    protected override void SetInteractionText()
    {
        interaction.SetInteractionText(" open door " + costText);
    }
}
