using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyBomb : TimedExplosion
{
    [Header("Events")]
    [SerializeField] protected TransformEventSO onMonkeyBombStartEvent;
    [SerializeField] protected TransformEventSO onMonkeyBombEndEvent;

    protected override void Start()
    {
        base.Start();
        onMonkeyBombStartEvent.Invoke(this.transform);
        Debug.Log("Monkey bomb start triggered");
    }

    public override void Explode()
    {
        onMonkeyBombEndEvent.Invoke(this.transform);
        base.Explode();
        Debug.Log("Monkey bomb end triggered");
    }
}
