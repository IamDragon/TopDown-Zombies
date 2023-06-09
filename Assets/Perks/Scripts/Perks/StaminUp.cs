using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Perks/StaminUp")]
public class StaminUp : Perk
{
    [SerializeField] private float speedMultiplier;
    private PlayerVariables playerStats;
    public override void ActivatePerk()
    {
        base.ActivatePerk();
        playerStats.speedMultiplier = speedMultiplier;
    }

    public override void DeactivatePerk()
    {
        base.DeactivatePerk();
        playerStats.speedMultiplier = 1;
    }

    public override void GetComponentHandler(GameObject player)
    {
        base.GetComponentHandler(player);
        playerStats = player.GetComponent<PlayerVariables>();
    }
}
