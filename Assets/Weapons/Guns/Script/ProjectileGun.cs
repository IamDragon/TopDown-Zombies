using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ProjectileGun : Gun
{
    [Header("Bullet")]
    [SerializeField] private Projectile bulletPrefab;

    protected override IEnumerator FireBullet(DamageInfo damageInfo)
    {
        base.FireBullet(damageInfo);
        isFiring = true;
        magAmmo -= 1;
        SpawnBullet(damageInfo);


        if (magAmmo <= 0)
        {
            StartReload();
            yield return new WaitForSeconds(0); // dont get shoot cooldown after reloading
        }
        else
            yield return new WaitForSeconds(secondsPerShot);

        isFiring = false;
    }

    private void SpawnBullet(DamageInfo damageInfo)
    {
        Vector2 dir = GetFiringDirection();
        Projectile bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.rotation);
        bullet.GetComponent<Movement>().SetVelocity(dir, bulletVelocity);
        bullet.damage = damage;
        bullet.damageInfo = damageInfo;
        bullet.range = range;
        bullet.headShotMultiplier = headShotMultiplier;
    }

    protected override void FireExtraBullet(DamageInfo damageInfo)
    {
        SpawnBullet(damageInfo);
        Debug.Log("ExtraBullet");
    }
}
