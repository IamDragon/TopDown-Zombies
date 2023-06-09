using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int points; //totalamount of points earned throughout the game
    public int kills;
    public int headshots;
    public int downs;

    [Header("Events")]
    [SerializeField] private PlayerStatsEventSO updateScoreboardEvent;
    [SerializeField] private EventSO enemyNormalDeathEvent;
    [SerializeField] private EventSO enemyHeadshotDeathEvent;
    [SerializeField] private EventSO onUpdatedowns;
    [SerializeField] private IntEventSO onPointIncrease;

    private void Start()
    {
        points = Player.Instance.PointManager.GetTotalPoints();
    }

    private void OnEnable()
    {
        enemyNormalDeathEvent.Action += IncreaseKillCount;
        enemyHeadshotDeathEvent.Action += IncreaseKillCount;
        enemyHeadshotDeathEvent.Action += IncreaseHeadshotCount;
        onUpdatedowns.Action += IncreaseDownCount;
        onPointIncrease.Action += IncreasPointCount;

        //EnemyActions.OnNormalDeath += IncreaseKillCount;
        //EnemyActions.OnHeadshotDeath += IncreaseKillCount;
        //EnemyActions.OnHeadshotDeath += IncreaseHeadshotCount;
        //PlayerActions.PlayerDowned += IncreaseDownCount;
        //PointManager.OnAddPoints += IncreasPointCount;
    }

    private void OnDisable()
    {
        enemyNormalDeathEvent.Action -= IncreaseKillCount;
        enemyHeadshotDeathEvent.Action -= IncreaseKillCount;
        enemyHeadshotDeathEvent.Action -= IncreaseHeadshotCount;
        onUpdatedowns.Action -= IncreaseDownCount;
        onPointIncrease.Action -= IncreasPointCount;

        //EnemyActions.OnNormalDeath -= IncreaseKillCount;
        //EnemyActions.OnHeadshotDeath -= IncreaseKillCount;
        //EnemyActions.OnHeadshotDeath -= IncreaseHeadshotCount;
        //PlayerActions.PlayerDowned -= IncreaseDownCount;
        //PointManager.OnAddPoints -= IncreasPointCount;
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

    private void IncreasPointCount(int amount)
    {
        points += amount;
        updateScoreboardEvent.Invoke(this);
    }

}
