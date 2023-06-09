using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{
    [Header("Invincibility")]
    [SerializeField] protected float invincibilityTime;
    protected bool invincible;

    protected HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem= GetComponent<HealthSystem>();
    }
    public virtual void GetHit(float damageAmount)
    {
        healthSystem.TakeDamage(damageAmount);
    }

    public virtual void GetHit(float damageAmount, Vector2 impactPoint)
    {
        GetHit(damageAmount);
    }
}
