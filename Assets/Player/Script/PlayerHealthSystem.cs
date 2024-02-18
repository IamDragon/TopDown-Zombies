using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem
{
    [Header("Downs")]
    [SerializeField] private int downsBeforeDeath;
    [SerializeField] private int downCount = 0;
    private StateManager stateManager;
    private PlayerVariables stats;

    [Header("Healing")]
    [SerializeField] private int healthGainPerInterval;
    [SerializeField] private float delayBeforeHealingStarts;
    [SerializeField] private float delayBetweenHealthGain;

    [Header("Events")]
    [SerializeField] protected EventSO onPlayerTrueDeath;
    [SerializeField] protected FloatFloatEventSO onPlayerHealthUpdate;

    public bool QuickReviveActive { get; set; }

    protected override void Start()
    {
        base.Start();
        stateManager = GetComponent<StateManager>();
        stats = GetComponent<PlayerVariables>();
    }

    protected override void Death()
    {
        base.Death();
        TrueDeath();

        //include if want player to be downed - don't have assets for this so wont be included
        //if (downCount >= downsBeforeDeath || !QuickReviveActive)
        //    TrueDeath();
        //else
        //    EnterDownState();
    }

    private void TrueDeath()
    {
        //end game if single player
        onPlayerTrueDeath.Invoke();
    }

    private void EnterDownState()
    {
        stateManager.ChangeState(1);
    }

    public void InitRevive()
    {
        Invoke(nameof(EnterStandingState), stats.reviveTime);
    }

    private void EnterStandingState()
    {
        stateManager.ChangeState(0);
    }

    private void StartHealing()
    {
        if (Health < currentMaxHealth)
        {
            Invoke(nameof(StartHealing), delayBetweenHealthGain);
            GainHealth(healthGainPerInterval);
            onPlayerHealthUpdate.Invoke(Health, currentMaxHealth);

        }
    }

    private void Heal()
    {
        GainHealth(healthGainPerInterval);
    }

    public override bool TakeDamage(float amount)
    {
        bool died = base.TakeDamage(amount);
        CancelInvoke(nameof(StartHealing));
        Invoke(nameof(StartHealing), delayBeforeHealingStarts);
        onPlayerHealthUpdate.Invoke(Health, currentMaxHealth);
        return died;
    }
}
