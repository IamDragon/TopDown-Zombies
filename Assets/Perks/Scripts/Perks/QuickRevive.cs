using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Perks/QuickRevive")]
public class QuickRevive : Perk
{
    PlayerHealthSystem healthSystem;
    public override void DeactivatePerk()
    {
        base.DeactivatePerk();
        healthSystem.QuickReviveActive = false;
        healthSystem.InitRevive();
    }

    public override void ActivatePerk()
    {
        healthSystem.QuickReviveActive = true;
        base.ActivatePerk();
    }

    public override void GetComponentHandler(GameObject player)
    {
        base.GetComponentHandler(player);
        healthSystem = player.GetComponent<PlayerHealthSystem>();
    }
}
