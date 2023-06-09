using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    [SerializeField] PointInfo pointInfo;
    [SerializeField] private int points;
    private int totalPoints;
    [SerializeField] private bool infinitePoints;
    private bool doublePointsActive;

    [Header("Events")]
    //enemy related
    [SerializeField] private EventSO enemyNormalDeathEvent;
    [SerializeField] private EventSO enemyHeadshotDeathEvent;
    [SerializeField] private EventSO enemyKnifeDeathEvent;
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
        enemyNormalDeathEvent.Action += AddKillPoints;
        enemyHeadshotDeathEvent.Action += AddSpecialKillPoints;
        enemyKnifeDeathEvent.Action += AddSpecialKillPoints;
        enemyHitEvent.Action += BodyShots;

        onDoublePointsStart.Action += ActivateDoublePoints;
        onDoublePointsEnd.Action += DeactivateDoublePoints;


        onNuke.Action += AddPoints;
        onCarpenter.Action += AddPoints;


        //EnemyActions.OnHeadshotDeath += AddSpecialKillPoints;
        //EnemyActions.OnKnifeDeath += AddSpecialKillPoints;
        //EnemyActions.OnNormalDeath += AddKillPoints;
        //EnemyActions.OnHit += BodyShots;

        //PowerUpActions.OnDoublePointsStart += ActivateDoublePoints;
        //PowerUpActions.OnDoublePointsEnd += DeactivateDoublePoints;


        //PowerUpActions.OnNukePoints += AddPoints;
        //PowerUpActions.OnCarpenterPoints += AddPoints;
    }

    private void OnDisable()
    {
        enemyNormalDeathEvent.Action -= AddKillPoints;
        enemyHeadshotDeathEvent.Action -= AddSpecialKillPoints;
        enemyKnifeDeathEvent.Action -= AddSpecialKillPoints;
        enemyHitEvent.Action -= BodyShots;

        onDoublePointsStart.Action -= ActivateDoublePoints;
        onDoublePointsEnd.Action -= DeactivateDoublePoints;


        onNuke.Action -= AddPoints;
        onCarpenter.Action -= AddPoints;

        //EnemyActions.OnHeadshotDeath -= AddSpecialKillPoints;
        //EnemyActions.OnKnifeDeath -= AddSpecialKillPoints;
        //EnemyActions.OnNormalDeath += AddKillPoints;
        //EnemyActions.OnHit -= BodyShots;

        //PowerUpActions.OnDoublePointsStart += ActivateDoublePoints;
        //PowerUpActions.OnDoublePointsEnd += DeactivateDoublePoints;

        //PowerUpActions.OnNukePoints -= AddPoints;
        //PowerUpActions.OnCarpenterPoints -= AddPoints;
    }

    private void Start()
    {
        totalPoints = points;
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
