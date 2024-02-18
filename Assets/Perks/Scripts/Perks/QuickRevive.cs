using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Perks/QuickRevive")]
public class QuickRevive : Perk
{
    [SerializeField] private float healingDelayDecrease; // should be negative
    [SerializeField] private float healingIntervalDecrease; // should be negative
    PlayerHealthSystem healthSystem;
    public override void DeactivatePerk()
    {
        base.DeactivatePerk();
        //since the values should be negative we need to make them positive to actually increase the values
        healthSystem.IncreaseHealingDelay(-healingDelayDecrease);
        healthSystem.IncreaseHealingInterval(-healingIntervalDecrease);

        //reving from quickRevive is disabled instead it just increases maxHealth
        //healthSystem.QuickReviveActive = false;
        //healthSystem.InitRevive();

    }

    public override void ActivatePerk()
    {
        //healthSystem.QuickReviveActive = true;
        healthSystem.IncreaseHealingDelay(healingDelayDecrease);
        healthSystem.IncreaseHealingInterval(healingIntervalDecrease);
        base.ActivatePerk();
    }

    public override void GetComponentHandler(GameObject player)
    {
        base.GetComponentHandler(player);
        healthSystem = player.GetComponent<PlayerHealthSystem>();
    }
}
