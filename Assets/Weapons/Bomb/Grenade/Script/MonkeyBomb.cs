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
        Debug.Log("Monkey bomb start triggered");
        onMonkeyBombStartEvent.Invoke(this.transform);
        base.Start();
    }

    public override void Explode()
    {
        Debug.Log("Monkey bomb end triggered");
        onMonkeyBombEndEvent.Invoke(this.transform);
        base.Explode();
    }
}
