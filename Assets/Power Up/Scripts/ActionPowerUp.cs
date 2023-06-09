using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPowerUp : PowerUp
{
    [Header("Events")]
    [SerializeField] protected EventSO onPowerUpActivate;

    protected override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        onPowerUpActivate.Invoke();
    }
}
