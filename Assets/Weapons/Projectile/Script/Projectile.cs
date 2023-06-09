using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public float damage = 0;
    public float headShotMultiplier;
    public float range = 0;
    private Vector3 previosuPos;
    private float totalDistanceTravelled;
    public DamageInfo damageInfo;
    private bool hasDoneDamage;

    public Sprite Sprite { get { return GetComponentInChildren<SpriteRenderer>().sprite; } }

    private void Start()
    {
        previosuPos= transform.position;
    }

    protected virtual void Update()
    {
        if (range == -1) return;
        Vector3 deltaPos = transform.position - previosuPos;
        totalDistanceTravelled += deltaPos.magnitude;
        previosuPos = transform.position;
        if(totalDistanceTravelled >= range)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        hasDoneDamage = true;
        if (collision.transform.CompareTag(damageInfo.tagToDamage) && !hasDoneDamage)
        {
            collision.transform.GetComponent<EnemyHitHandler>().GetHit(damage, transform.position);
        }
        else if (collision.transform.CompareTag(damageInfo.headTag))
        {
            collision.transform.GetComponent<EnemyHitHandler>().GetHitHeadshot(damage, headShotMultiplier, transform.position);
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        hasDoneDamage = true;
        if (collision.transform.CompareTag(damageInfo.tagToDamage) && !hasDoneDamage)
        {
            collision.transform.GetComponent<EnemyHitHandler>().GetHit(damage, transform.position);
        }
        else if (collision.transform.CompareTag(damageInfo.headTag))
        {
            collision.transform.GetComponent<EnemyHitHandler>().GetHitHeadshot(damage, headShotMultiplier, transform.position);
        }
    }
}
