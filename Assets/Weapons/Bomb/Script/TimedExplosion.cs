using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedExplosion : Explosion
{
    [SerializeField] float fuseTime;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(StartTimer());
    }

    public IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(fuseTime);
        Explode();
        Destroy(gameObject);
    }
}
