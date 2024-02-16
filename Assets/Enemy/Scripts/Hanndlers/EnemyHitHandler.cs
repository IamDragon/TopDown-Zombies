// Ignore Spelling: Insta Headshot

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitHandler : HitHandler
{
    public bool instaKillActive;
    private HitType lastHit;
    private EnemyAudioManager audioManager;

    [Header("Events")]
    [SerializeField] protected EventSO onInstaKillStartEvent;
    [SerializeField] protected EventSO onInstaKillEndEvent;
    [SerializeField] protected EnemyHitHandlerEventSO onDeath;
    [SerializeField] protected EventSO onHitEvent;

    public enum HitType
    {
        Normal,
        Special,
        Headshot
    }

    public HitType LastHit { get { return lastHit; } }

    private void Start()
    {
        audioManager = GetComponent<EnemyAudioManager>();
    }
    private void OnEnable()
    {
        healthSystem.OnDeath += Die;
        onInstaKillStartEvent.Action += ActivateInstaKill;
        onInstaKillEndEvent.Action += DeactivateInstaKill;
    }

    private void OnDisable()
    {
        healthSystem.OnDeath -= Die;
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
        lastHit = HitType.Special;
        TakeDamage(damageAmount, impactPoint);
    }

    private void TakeDamage(float damageAmount, Vector2 impactPoint)
    {
        AnimatedVFXManager.Instance.PlayVFX(AnimatedVFXManager.VFXType.Blood, impactPoint, transform.rotation);
        audioManager.PlayHurtSound();
        if (instaKillActive)
            healthSystem.TakeDamage(healthSystem.Health);
        else
        {
            if (!healthSystem.TakeDamage(damageAmount))
                onHitEvent.Invoke();
        }
    }

    private void Die()
    {
        onDeath.Invoke(this);
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
