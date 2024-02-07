using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private int timeBetweenRounds;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform defaultSpawnPoint;
    private EnemyMangager enemyManager;
    public int enemiesAlive;
    public int enemiesLeftToSpawn;
    public int currentRound;
    public Pathfinding pathFinder;

    //delayed enemy spawning
    [SerializeField] private float spawnDelay;
    [SerializeField] private float maxDistToSpawn;

    [Header("Events")]
    [SerializeField] protected IntEventSO onRoundEnd;

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
        EndRound(); // start game by ending round 0 

    }

    private void EndRound()
    {
        Debug.Log("End round");
        onRoundEnd.Invoke(currentRound + 1); // currentRound wouldnt have been updated yet
        Invoke(nameof(StartNextRound), timeBetweenRounds);
    }

    private void StartNextRound()
    {
        currentRound++;
        enemiesLeftToSpawn = CalculateEnemiesThisRound();
        SpawnEnemies();
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
            Debug.LogWarning("No spawnpoint found return defaultSpawnPoint");
            return defaultSpawnPoint.position;
        }
        return pointsForSpawning[UnityEngine.Random.Range(0, pointsForSpawning.Count - 1)].position;
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
        if (enemiesLeftToSpawn > 0) // still have enemies left to spawn
            SpawnRemainingEnemies();
        else if (enemiesLeftToSpawn <= 0 && enemiesAlive <= 0) //nothing to spawn and nothing alive -> round is done
            EndRound();
    }

    private int CalculateEnemiesThisRound()
    {
        //formula for 1 player
        int enemiesThisRound = Mathf.RoundToInt(0.000058f * Mathf.Pow(currentRound, 3f) + 0.074032f * Mathf.Pow(currentRound, 2) + 0.718119f * currentRound + 14.738699f);
        //Debug.Log("Zombies on round "+currentRound+": " + enemiesThisRound);
        return enemiesThisRound;
    }

    private void SpawnEnemies()
    {
        enemiesLeftToSpawn = CalculateEnemiesThisRound();
        int amountToSpawn = enemiesLeftToSpawn;
        if (amountToSpawn <= enemyManager.MaxEnemiesAlive) //less enemies to spawn than total enemies allowed -> spawn all enemies
        {
            for (int i = 0; i < amountToSpawn; i++)
            {
                SpawnEnemy(GetSpawnPoint());
            }
        }
        else
        {
            for (int i = 0; i < enemyManager.MaxEnemiesAlive; i++) //if more enemies spawn than allowed to be alive at once -> spawn max allowed enemies{
            {
                SpawnEnemy(GetSpawnPoint());
            }
        }
    }

    //This function will be called when a zombie dies and every x seconds in case an error occurs during spawning -> keep game going with correct amount of zombies even if spawning error occurs
    private void SpawnRemainingEnemies()
    {
        //if already max alive just return
        if (enemiesAlive == enemyManager.MaxEnemiesAlive) //already max enemies alive dont spawn more
            return;
        int maxAmountToSpawn = enemyManager.MaxEnemiesAlive - enemiesAlive;
        if (maxAmountToSpawn >= enemiesLeftToSpawn) // spawn thr rest of the enemies for the round
        {
            for (int i = 0; i < enemiesLeftToSpawn; i++)
            {
                SpawnEnemy(GetSpawnPoint());
            }
        }
        else //less -> spawn max amount to fill up
        {
            for (int i = 0; i < maxAmountToSpawn; i++)
            {
                SpawnEnemy(GetSpawnPoint());
            }
        }
    }

    private void SpawnEnemy(Vector3 spawnPoint)
    {
        Enemy enemy = enemyManager.GetInactiveEnemy();
        if (enemy == null) return; // enemy not found -> do not do anything to indicate that one has been spawned

        //update relevant variables to ensure  the right amount is spawned every round
        enemiesLeftToSpawn--;
        enemiesAlive++;
        enemy.ResetSelf(spawnPoint);
        return;
    }
}
