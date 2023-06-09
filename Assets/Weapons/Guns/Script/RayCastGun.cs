using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class RayCastGun : Gun
{
    [SerializeField] private LayerMask stoppingLayer;
    [SerializeField] private TrailRenderer bulletTrail;
    protected LayerMask hitMask;

    protected void CalculateHitMask()
    {
        //hitMask = new LayerMask();
        hitMask = stoppingLayer | damageInfo.maskToDamage;
    }

    protected override IEnumerator FireBullet(DamageInfo damageInfo)
    {
        isFiring = true;
        magAmmo -= 1;

        CalculateHits(damageInfo);

        if (magAmmo <= 0)
        {
            StartReload();
            yield return new WaitForSeconds(0); // dont get shoot cooldown after reloading
        }
        else
        {
            yield return new WaitForSeconds(secondsPerShot);
        }

        isFiring = false;
    }

    protected virtual void CalculateHits(DamageInfo damageInfo)
    {
        Vector2 dir = CalculateBulletDir();
        RaycastHit2D[] hits = Physics2D.RaycastAll(firePoint.position, dir, range, hitMask);
        Vector2 impactPoint = Vector2.zero;
        //if (hits.Length > 0) // if we hit anything check for walls
        //{
        //    CheckForWallHit(ref hits);
        //    //impactPoint;
        //}
        if (CheckForWallHit(ref hits))
        {
            impactPoint = hits[hits.Length - 1].point; // wall will be last element
            Debug.Log(impactPoint);
            Debug.DrawRay(transform.position, dir * Vector2.Distance(firePoint.position, impactPoint), Color.red, 0.1f);



            Array.Resize(ref hits, hits.Length - 1);
        }
        TrailRenderer trailRenderer = Instantiate(bulletTrail, firePoint.position, firePoint.rotation);

        if (impactPoint != Vector2.zero)
        {
            StartCoroutine(SpawnTrail(trailRenderer, impactPoint));

        }
        else
        {
            Vector2 endpoint = transform.position;
            endpoint += dir * range;
            StartCoroutine(SpawnTrail(trailRenderer, endpoint));
        }


        List<Collider2D> enemyHits = HelperFunctions.RemoveDuplicateColliders(hits);

        foreach (Collider2D hit in enemyHits)
        {
            if (hit.CompareTag(damageInfo.headTag))
            {
                hit.transform.parent.GetComponent<EnemyHitHandler>().GetHitHeadshot(damage, headShotMultiplier, hit.transform.position);
            }
            else
            {
                hit.transform.parent.GetComponent<EnemyHitHandler>().GetHit(damage, hit.transform.position);
            }
        }

        if (enemyHits.Count > 0)
            Debug.DrawRay(firePoint.position, dir * range, Color.green, 0.1f);
        else if (impactPoint != Vector2.zero)
            Debug.DrawRay(firePoint.position, dir * Vector2.Distance(firePoint.position, impactPoint), Color.red, 0.1f);

        else
            Debug.DrawRay(firePoint.position, dir * range, Color.red, 0.1f);
    }

    protected override void FireExtraBullet(DamageInfo damageInfo)
    {
        CalculateHits(damageInfo);
        Debug.Log("ExtraBullet");
    }

    /// <summary>
    /// if hit a wall returns true, last elemt will be the wall that was hit
    /// </summary>
    /// <param name="hits"></param>
    /// <returns></returns>
    protected bool CheckForWallHit(ref RaycastHit2D[] hits)
    {
        if (hits.Length == 0) return false; //nothing to check

        int stopIndex = hits.Length; //if no walls are hit but only enemies problems will be cause -> stopHit = hits[stopIndex];
        //RaycastHit2D stopHit;
        for (int i = 0; i < hits.Length; i++)
        {
            if (((1 << hits[i].collider.gameObject.layer) & stoppingLayer) != 0)
            {
                stopIndex = i;
                Array.Resize(ref hits, stopIndex + 1);
                Debug.Log(true);
                return true;
            }
        }
        //stopHit = hits[stopIndex];
        //if (stopIndex == 0)//first thing hit is wall
        //hits = null;

        //else if (stopIndex - 1 >= 0)
        //Array.Resize(ref hits, stopIndex + 1);
        //return stopHit;
        return false;
    }

    public override void Init(WeaponHandler weaponHandler, bool isDowenedGun)
    {
        base.Init(weaponHandler, isDowenedGun);
        CalculateHitMask();
    }


    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 endpoint)
    {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while(time<1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, endpoint, time);
            time += Time.deltaTime /trail.time;

            yield return null;
        }

        trail.transform.position = endpoint;

        Destroy(trail.gameObject, trail.time);
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector2 endpoint)
    {
        Vector3 startPosition = trail.transform.position;
        float distance = Vector2.Distance(trail.transform.position, endpoint);
        float remaningDistance = distance;

        while (remaningDistance > 0)
        {
            trail.transform.position = Vector2.Lerp(startPosition, endpoint, 1 - remaningDistance / distance);
            //time += Time.deltaTime / trail.time;
            remaningDistance -= bulletVelocity* Time.deltaTime;

            yield return null;
        }

        trail.transform.position = endpoint;

        Destroy(trail.gameObject, trail.time);
    }
}
