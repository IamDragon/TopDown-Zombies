using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] protected DamageInfo damageInfo;
    [SerializeField] protected float explosionRadius;
    [SerializeField] protected float damage;
    [SerializeField] protected AnimatedVFXManager.VFXType explosionType;
    [SerializeField] protected AudioClip[] explosionSounds;


    protected virtual void Start()
    {
    }

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
        PositionalAudioManager.Instance.PlayAudio(transform.position, explosionSounds[Random.Range(0, explosionSounds.Length - 1)]);

        Destroy(gameObject);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
