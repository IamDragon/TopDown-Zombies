// Ignore Spelling: Insta Headshot

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitHandler : HitHandler
{
    public bool instaKillActive;
    private HitType lastHit;

    [Header("Events")]
    [SerializeField] protected EventSO onInstaKillStartEvent;
    [SerializeField] protected EventSO onInstaKillEndEvent;
    [SerializeField] protected EventSO onNormalDeathEvent;
    [SerializeField] protected EventSO onSpecialDeathEvent;
    [SerializeField] protected EventSO onHeadshotDeathEvent;
    [SerializeField] protected EventSO onHitEvent;

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
        TakeDamage(damageAmount, impactPoint);
    }
    public void GetHitHeadshot(float damageAmount, float headshotMultiplier, Vector2 impactPoint)
    {
        lastHit = HitType.Headshot;
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
                onHitEvent.Invoke();
        }
    }

    private void CalculateDeathCause()
    {
        switch (lastHit)
        {
            case HitType.Normal:
                onNormalDeathEvent.Invoke();
                break;
            case HitType.Headshot:
                onHeadshotDeathEvent?.Invoke();
                break;
            case HitType.Knife:
                onSpecialDeathEvent.Invoke();
                break;
        }
    }

    private void ActivateInstaKill()
    {
        Debug.Log("a");
        instaKillActive = true;
    }

    private void DeactivateInstaKill()
    {
        instaKillActive = false;
    }
}
