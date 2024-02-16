using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static EnemyHitHandler;

public class PointManager : MonoBehaviour
{
    [SerializeField] PointInfo pointInfo;
    [SerializeField] private int points;
    private int totalPoints;
    [SerializeField] private bool infinitePoints;
    private bool doublePointsActive;

    [Header("Events")]
    //enemy related
    [SerializeField] private EnemyHitHandlerEventSO onEnemyDeath;
    [SerializeField] private EventSO enemyHitEvent;

    //points increa/decrease
    [SerializeField] private IntEventSO onAddPoints;
    [SerializeField] private IntEventSO onRemovePoints;

    //Powerup events
    [SerializeField] private EventSO onDoublePointsStart;
    [SerializeField] private EventSO onDoublePointsEnd;
    [SerializeField] private IntEventSO onNuke;
    [SerializeField] private IntEventSO onCarpenter;

    public static Action<int> OnAddPoints;
    public static Action<int> OnRemovePoints;

    private void OnEnable()
    {
        onEnemyDeath.Action += AddKillPoints;
        enemyHitEvent.Action += BodyShots;

        onDoublePointsStart.Action += ActivateDoublePoints;
        onDoublePointsEnd.Action += DeactivateDoublePoints;


        onNuke.Action += AddPoints;
        onCarpenter.Action += AddPoints;

    }

    private void OnDisable()
    {
        onEnemyDeath.Action -= AddKillPoints;
        enemyHitEvent.Action -= BodyShots;

        onDoublePointsStart.Action -= ActivateDoublePoints;
        onDoublePointsEnd.Action -= DeactivateDoublePoints;


        onNuke.Action -= AddPoints;
        onCarpenter.Action -= AddPoints;

    }

    private void Start()
    {
        totalPoints = points;
        onAddPoints.Invoke(totalPoints);
    }

    public void AddPoints(int amount)
    {
        int amountToAdd;
        if (doublePointsActive)
        {
            amountToAdd = amount * 2;
            //Debug.Log("Added points " + amount * 2);
        }
        else
        {
            //Debug.Log("Added points " + amount);
            amountToAdd = amount;

        }

        points += amountToAdd;
        totalPoints += amountToAdd;
        onAddPoints.Invoke(amountToAdd);
    }

    private void BodyShots() // need better name - adds points for damaging
    {
        AddPoints(pointInfo.hitPoints);
    }

    private void AddKillPoints()
    {
        AddPoints(pointInfo.killPoints);
    }

    private void AddSpecialKillPoints()
    {
        AddPoints(pointInfo.headshotPoints);
    }

    private void AddKillPoints(EnemyHitHandler hithandler)
    {
        switch (hithandler.LastHit)
        {
            case HitType.Normal:
                AddPoints(pointInfo.killPoints);
                break;
            case HitType.Headshot:
                AddPoints(pointInfo.headshotPoints);
                break;
            case HitType.Special:
                AddPoints(pointInfo.headshotPoints);
                break;
        }
    }


    public int GetPoints()
    {
        if (infinitePoints)
            return int.MaxValue;
        return points;
    }

    public int GetTotalPoints()
    {
        return totalPoints;
    }

    public void RemovePoints(int amount)
    {
        if (infinitePoints) return;
        points -= amount;
        onRemovePoints?.Invoke(amount);
    }

    private void ActivateDoublePoints()
    {
        doublePointsActive = true;
    }

    private void DeactivateDoublePoints()
    {
        doublePointsActive = false;
    }

    public bool CanAfford(int cost)
    {
        if (infinitePoints) return true;
        return points >= cost;
    }
}
