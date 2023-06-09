using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPowerUp : PowerUp
{
    [Header("Points")]
    [SerializeField] protected int points;

    [Header("Events")]
    [SerializeField] protected IntEventSO onPowerUpActivate;

    protected override void ActivatePowerUp()
    {
        base.ActivatePowerUp();
        onPowerUpActivate.Invoke(points);
    }

}
