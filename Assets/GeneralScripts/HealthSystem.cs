using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    /// <summary>
    /// The baseline max health which is not ever increased
    /// </summary>
    [SerializeField] protected float baseMaxHealth;
    /// <summary>
    /// The current max health which can fluctuate during runtime
    /// </summary>
    [SerializeField] protected float currentMaxHealth;
    /// <summary>
    /// The absolute max health which can never be passed
    /// </summary>
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [Header("Invincibility")]
    [SerializeField] private float invincibilityTime;
    [SerializeField] private bool invincible;
    private bool isAlive;
    public Action OnDeath;
    public Action OnTakeDamage;

    public float MaxHealth { get { return maxHealth; } }
    public float Health { get { return health; } }
    public bool IsAlive { get { return isAlive; } set { isAlive = value; }}

    protected virtual void Start()
    {
        health = currentMaxHealth;
    }

    public virtual void GainHealth(float amount)
    {
        health += amount;
        if (health > currentMaxHealth)
        {
            health = currentMaxHealth;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="amount"></param>
    /// <returns>true if entity is "killed" otherwise false</returns>
    public virtual bool TakeDamage(float amount)
    {
        if (invincible) return false;
        health -= amount;
        if (health <= 0)
        {
            Death();
            return true;
        }
        if(invincibilityTime > 0)
            Invoke(nameof(StopInvincibility), invincibilityTime);

        return false;
    }

    private void StopInvincibility()
    {
        invincible = false;
    }

    public void Init(float baseHealth)
    {
        SetBaseMaxHealth(baseHealth);
        SetCurrentMaxHealth(baseHealth);
        health = currentMaxHealth;
    }

    protected virtual void Death()
    {
        OnDeath?.Invoke();
        isAlive= false;
        //Debug.Log(this + " Death");
    }

    public void ResetHealth()
    {
        health = currentMaxHealth;
        isAlive= true;
    }

    /// <summary>
    /// Sets baseMaxHealth to the desired amount,
    /// if amount i larger than maxHealth, baseMaxHealth is instead set to maxHealth
    /// </summary>
    /// <param name="amount"></param>
    private void SetBaseMaxHealth(float amount)
    {
        if (amount > maxHealth)
            baseMaxHealth = maxHealth;
        else
            baseMaxHealth = amount;
    }

    /// <summary>
    /// Sets currentMaxHealth to the desired amount,
    /// if amount i larger than maxHealth, currentMaxHealth is instead set to maxHealth
    /// </summary>
    /// <param name="amount"></param>
    public void SetCurrentMaxHealth(float amount)
    {
        if (amount > maxHealth)
            currentMaxHealth = maxHealth;
        else
        currentMaxHealth = amount;
    }

    /// <summary>
    /// Sets currentMaxHealth to baseMaxHealth
    /// </summary>
    public void ResetCurrentMaxHealth()
    {
        currentMaxHealth = baseMaxHealth;
    }
}
