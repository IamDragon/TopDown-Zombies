using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkHandler : MonoBehaviour
{
    public List<Perk> perks;
    [SerializeField] private int maxPerks;
    public void RecievePerk(Perk perk)
    {
        perks.Add(perk);
        perk.GetComponentHandler(this.gameObject); // this is attached to the player
        perk.ActivatePerk();
    }

    public void DeactivatePerks()
    {
        foreach (Perk perk in perks)
        {
            perk.DeactivatePerk();
        }
        perks.Clear();
    }

    public bool CanPurchasePerk(Perk perk)
    {
        if (!perks.Contains(perk) && perks.Count < maxPerks)
            return true;
        return false;
    }
}
