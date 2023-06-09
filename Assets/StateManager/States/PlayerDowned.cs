using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "State/PlayerDowned")]
public class PlayerDowned : State
{
    private WeaponHandler weaponHandler;
    private PlayerVariables playerStats;
    private PerkHandler perkHandler;

    [Header("Events")]
    [SerializeField] protected EventSO onPlayerDownedEvent;
    [SerializeField] protected TransformEventSO onPlayerRevivedEvent;

    public override void Enter()
    {
        //set weapon handler to be starterGun or the first found pistol
        weaponHandler.SwitchToDownedGun();
        playerStats.isDowned = true; //move to playerHealthSystem
        perkHandler.DeactivatePerks();
        onPlayerDownedEvent.Invoke();
    }

    public override void Exit()
    {
        //reactivate weapons player had before being downed
        weaponHandler.SwitchToStandingGuns();
        playerStats.isDowned = false;
        onPlayerRevivedEvent.Invoke(stateManager.transform);
    }

    protected override void GetRelevantComponents()
    {
        weaponHandler = stateManager.transform.GetComponentInChildren<WeaponHandler>();
        playerStats = stateManager.GetComponent<PlayerVariables>();
        perkHandler = stateManager.GetComponent<PerkHandler>();
    }
}
