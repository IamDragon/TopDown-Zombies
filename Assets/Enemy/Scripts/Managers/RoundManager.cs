using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private int enemiesPerRound;
    [SerializeField] private int timeBetweenRounds;
    [SerializeField] private Transform[] spawnPoints;
    private EnemyMangager enemyManager;
    public int enemiesAlive;
    public int enemiesLeftToSpawn;
    public int currentRound;
    public Pathfinding pathFinder;

    //delayed enemy spawning
    public int waitingToSpawn; // how many enemies r waiting to spawn
    [SerializeField] private float spawnDelay;
    [SerializeField] private float maxDistToSpawn;

    [Header("Events")]
    [SerializeField] protected IntEventSO onRoundEnd;

    private bool roundStarting;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyMangager>();
        pathFinder = GetComponent<Pathfinding>();
    }

    private void Start()
    {
        currentRound = 0;
        foreach (Enemy e in enemyManager.enemies)
            e.HealthSystem.OnDeath += EnemyDied;

    }

    private void Update()
    {

        if (enemiesLeftToSpawn == 0 && enemiesAlive == 0 && roundStarting == false)
        {
            EndRound();
        }
    }

    private void EndRound()
    {
        Debug.Log("End round");
        roundStarting = true;
        Invoke(nameof(StartNextRound), timeBetweenRounds);
        onRoundEnd.Invoke(currentRound + 1); // currentRound wouldnt have been updated yet
    }

    private void StartNextRound()
    {
        currentRound++;
        enemiesLeftToSpawn = CalculateEnemiesThisRound();
        SpawnEnemies();
        Debug.Log("roundStartingTrue");
        roundStarting = false;
    }

    private void NewSpawnEnemies()
    {


    }

    private void SpawnEnemies()
    {
        Debug.Log("spawning enemies");
        int amountToSpawn;
        int maxSpawn = enemyManager.MaxEnemiesAlive - enemiesAlive;

        if (maxSpawn <= enemiesLeftToSpawn)
        {
            amountToSpawn = maxSpawn;
        }
        else//maxSpawn > enemiesLeftToSpawn
        {
            amountToSpawn = enemiesLeftToSpawn;
        }
        Debug.Log("amountToSpawn " + amountToSpawn);

        waitingToSpawn = amountToSpawn;
        for (int i = 0; i < amountToSpawn; i++)
        {
            Invoke(nameof(SpawnEnemy), spawnDelay);
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = enemyManager.GetInactiveEnemy();
        Vector3 spawnPoint = GetSpawnPoint();
        if (enemy != null)
        {
            enemy.ResetSelf(spawnPoint);
            enemiesLeftToSpawn--;
            enemiesAlive++;
        }
        else
        {
            Debug.LogWarning("Enemy not found and could not be spawned");
        }
        waitingToSpawn--;
        if (waitingToSpawn < 0)
            waitingToSpawn = 0;
    }

    /// <summary>
    /// Returns a spawnpoint whihc is in the defined range and from which and enemy can reach the player
    /// </summary>
    /// <returns>if no point is found returns vector.zero</returns>
    private Vector3 GetSpawnPoint() //may not find a spawnPoint if the player ever exits the range
    {
        List<Transform> pointsForSpawning = GetSpawnPointsInRange();
        //return random position from list
        if (pointsForSpawning.Count == 0)
        {
            Debug.LogWarning("No spawnpoint found return Vector3.zero");
            return Vector3.zero;
        }
        return pointsForSpawning[UnityEngine.Random.Range(0, pointsForSpawning.Count-1)].position;
    }

    private bool IsSpawnPointInvalid(Transform spawnPoint)
    {
        if (pathFinder.FindPath(spawnPoint.position, enemyManager.Player.position) != null)
            return false;
        return true;
    }

    private List<Transform> GetSpawnPointsInRange()
    {
        List<Transform> spawnPointsList = new List<Transform>();

        //find all in range
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (Vector3.Distance(spawnPoints[i].position, enemyManager.Player.position) <= maxDistToSpawn) // if t
                spawnPointsList.Add(spawnPoints[i]);
        }

        //remove all invalid spawnpoints
        spawnPointsList.RemoveAll(IsSpawnPointInvalid);

        return spawnPointsList;

    }

    private void EnemyDied()
    {
        enemiesAlive--;
        SpawnEnemies();
    }

    private int CalculateEnemiesThisRound()
    {
        //formula for 1 player
        return Mathf.RoundToInt(0.000058f * Mathf.Pow(currentRound,3f) + 0.074032f * Mathf.Pow(currentRound,2) + 0.718119f * currentRound + 14.738699f);
    }
}
