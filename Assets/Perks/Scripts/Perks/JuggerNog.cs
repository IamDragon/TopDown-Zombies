using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Perks/JuggerNog")]
public class JuggerNog : Perk
{
    [SerializeField] private float healthGain;
    private HealthSystem healthSystem;

    public override void ActivatePerk()
    {
        base.ActivatePerk();
        if (healthSystem != null)
            healthSystem.SetCurrentMaxHealth(healthSystem.MaxHealth + healthGain);
        else
            Debug.LogWarning("HealthSystem was null");
    }

    public override void DeactivatePerk()
    {
        base.DeactivatePerk();
        healthSystem.ResetCurrentMaxHealth();
    }

    public override void GetComponentHandler(GameObject player)
    {
        healthSystem = player.GetComponent<HealthSystem>();
    }
}
