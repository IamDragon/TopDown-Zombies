using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] protected DamageInfo damageInfo;
    [SerializeField] protected float explosionRadius;
    [SerializeField] protected float damage;
    [SerializeField] protected AnimatedVFXManager.VFXType explosionType;
    

    public virtual void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageInfo.maskToDamage);
        List<Transform> list = HelperFunctions.GetTransformParentFromColliders(hits);


        foreach (Transform t in list)
        {
            t.GetComponent<EnemyHitHandler>().GetHit(damage);

        }

        //randomize rotation?
        //
        AnimatedVFXManager.Instance.PlayVFX(explosionType, transform.position, transform.rotation);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
