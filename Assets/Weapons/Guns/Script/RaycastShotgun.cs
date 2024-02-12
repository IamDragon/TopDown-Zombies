using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShotgun : RayCastGun
{
    [SerializeField] private int bulletCount;
    [SerializeField] private float maxAngle;

    protected override IEnumerator FireBullet(DamageInfo damageInfo)
    {
        audioPlayer.PlayRandomAudio();
        isFiring = true;
        magAmmo -= 1;

        for (int i = 0; i < bulletCount; i++)
        {
            CalculateHits(damageInfo);
        }

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
    protected override Vector2 CalculateBulletDir()
    {
        
        Vector2 dirToMouse = GetFiringDirection();
        dirToMouse.x += Random.Range(-maxAngle, maxAngle);
        dirToMouse.y += Random.Range(-maxAngle, maxAngle);
        return dirToMouse.normalized;
    }
}
