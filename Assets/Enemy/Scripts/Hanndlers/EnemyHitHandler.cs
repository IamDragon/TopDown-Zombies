using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitHandler : HitHandler
{
    bool instaKillActive;
    private HitType lastHit;

    [Header("Events")]
    [SerializeField] protected EventSO onInstaKillStartEvent;
    [SerializeField] protected EventSO onInstaKillEndEvent;

    private enum HitType
    {
        Normal,
        Knife,
        Headshot
    }

    private void Start()
    {
    }
    private void OnEnable()
    {
        healthSystem.OnDeath += CalculateDeathCause;
        onInstaKillStartEvent.Action += ActivateInstaKill;
        onInstaKillEndEvent.Action += DeactivateInstaKill;
    }

    private void OnDisable()
    {
        healthSystem.OnDeath -= CalculateDeathCause;
        onInstaKillStartEvent.Action -= ActivateInstaKill;
        onInstaKillEndEvent.Action -= DeactivateInstaKill;
    }

    public override void GetHit(float damageAmount, Vector2 impactPoint)
    {
        lastHit = HitType.Normal;
        //AnimatedVFXManager.Instance.PlayVFX(AnimatedVFXManager.VFXType.blood, impactPoint, transform.rotation);
        TakeDamage(damageAmount, impactPoint);
    }
    public void GetHitHeadshot(float damageAmount, float headshotMultiplier, Vector2 impactPoint)
    {
        lastHit = HitType.Headshot;
        //AnimatedVFXManager.Instance.PlayVFX(AnimatedVFXManager.VFXType.blood, impactPoint, transform.rotation);
        TakeDamage(damageAmount * headshotMultiplier,impactPoint);
    }

    public void GetHitMelee(float damageAmount, Vector2 impactPoint)
    {
        lastHit = HitType.Knife;
        TakeDamage(damageAmount, impactPoint);
    }

    private void TakeDamage(float damageAmount, Vector2 impactPoint)
    {
        AnimatedVFXManager.Instance.PlayVFX(AnimatedVFXManager.VFXType.Blood, impactPoint, transform.rotation);
        if (instaKillActive)
            healthSystem.TakeDamage(healthSystem.Health);
        else
        {
            if (!healthSystem.TakeDamage(damageAmount))
                EnemyActions.OnHit?.Invoke();
        }
    }

    private void CalculateDeathCause()
    {
        switch (lastHit)
        {
            case HitType.Normal:
                EnemyActions.OnNormalDeath?.Invoke();
                break;
            case HitType.Headshot:
                EnemyActions.OnHeadshotDeath?.Invoke();
                break;
            case HitType.Knife:
                EnemyActions.OnKnifeDeath?.Invoke();
                break;
        }
    }

    private void ActivateInstaKill()
    {
        instaKillActive = true;
    }

    private void DeactivateInstaKill()
    {
        instaKillActive = false;
    }
}
