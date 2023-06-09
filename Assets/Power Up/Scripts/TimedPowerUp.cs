using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedPowerUp : PowerUp
{
    //methods overriden in sub classes to implement own action calls

    [Header("Duration")]
    [SerializeField] protected float duration;

    [Header("Events")]
    [SerializeField] protected EventSO onPowerUpStart;
    [SerializeField] protected EventSO onPowerUpEnd;


    protected override void ActivatePowerUp()
    {
        Invoke(nameof(DeactivatePowerUp), duration);
        onPowerUpStart.Invoke();
        Debug.Log(this + " start");
    }

    protected virtual void DeactivatePowerUp()
    {
        onPowerUpEnd.Invoke();
        Debug.Log(this + " end");
    }
}
