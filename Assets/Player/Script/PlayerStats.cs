using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyHitHandler;

public class PlayerStats : MonoBehaviour
{
    public int points; //totalamount of points earned throughout the game
    public int kills;
    public int headshots;
    public int downs;

    [Header("Events")]
    [SerializeField] private PlayerStatsEventSO updateScoreboardEvent;
    [SerializeField] private EnemyHitHandlerEventSO OnEnemyDeath;
    [SerializeField] private EventSO onUpdatedowns;
    [SerializeField] private IntEventSO onPointIncrease;

    private void Start()
    {
        points = Player.Instance.PointManager.GetTotalPoints();
    }

    private void OnEnable()
    {
        OnEnemyDeath.Action += CalculateKill;
        onUpdatedowns.Action += IncreaseDownCount;
        onPointIncrease.Action += IncreasPointCount;

    }

    private void OnDisable()
    {
        OnEnemyDeath.Action -= CalculateKill;
        onUpdatedowns.Action -= IncreaseDownCount;
        onPointIncrease.Action -= IncreasPointCount;

    }

    private void IncreaseKillCount()
    {
        kills++;
        updateScoreboardEvent.Invoke(this);
    }

    private void IncreaseHeadshotCount()
    {
        headshots++;
        updateScoreboardEvent.Invoke(this);
    }

    private void IncreaseDownCount()
    {
        downs++;
        updateScoreboardEvent.Invoke(this);
    }

    private void CalculateKill(EnemyHitHandler hitHandler)
    {
        switch (hitHandler.LastHit)
        {
            case HitType.Normal:
                IncreaseKillCount();
                IncreaseHeadshotCount();
                break;
            case HitType.Headshot:
                IncreaseHeadshotCount();
                break;
        }
    }

    private void IncreasPointCount(int amount)
    {
        points += amount;
        updateScoreboardEvent.Invoke(this);
    }

}
