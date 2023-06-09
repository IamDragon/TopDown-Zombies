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
        if(downCount >= downsBeforeDeath  || !QuickReviveActive )
            TrueDeath();
        else
            EnterDownState();
    }

    private void TrueDeath()
    {
        //end game if single player
        Debug.Log("True death");
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
        return died;
    }
}
