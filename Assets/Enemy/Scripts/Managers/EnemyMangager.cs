using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using AStar;
//using Pathfinding;

public class EnemyMangager : MonoBehaviour
{
    [SerializeField] private float timeBetweenPathUpdate;
    [SerializeField] private Transform player;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int maxEnemiesAlive;
    [SerializeField] private int totalAMountToPool; //number of enemies that are pooled, should be higher than maxenemiesAlive

    [Header("Enemy HP")]
    [SerializeField] private float baseHealth;
    private float currentHealth;

    public Enemy[] enemies;

    private Pathfinding pathfinder;

    public List<Transform> targets;
    public Transform currentTarget;
    bool playerAlive;

    [Header("Events")]
    //player related
    [SerializeField] protected EventSO onPlayerDownedEvent;
    [SerializeField] protected TransformEventSO onPlayerRevivedEvent;

    //power up related event
    [SerializeField] protected IntEventSO onNukeEvent;

    //Monkey bomb event
    [SerializeField] protected TransformEventSO onMonkeyStartEvent;
    [SerializeField] protected TransformEventSO onMonkeyEndEvent;

    //Round events
    [SerializeField] protected IntEventSO onRoundEnd;

    //gameover event
    [SerializeField] protected EventSO onGameOver;

    //Properties
    public int MaxEnemiesAlive { get { return maxEnemiesAlive; } }

    public Transform Player { get { return player; } }

    private void Awake()
    {
        pathfinder = GetComponent<Pathfinding>();
        enemies = new Enemy[totalAMountToPool];
        targets = new List<Transform>();
        currentTarget = player;
        playerAlive = true;
        CreateEnemies();
    }



    private void OnEnable()
    {
        onNukeEvent.Action += KillAll;
        onMonkeyStartEvent.Action += TargetMonkey;
        onMonkeyEndEvent.Action += RemoveMonkeyTarget;
        onPlayerDownedEvent.Action += RemovePlayer;
        onPlayerRevivedEvent.Action += AddPlayer;
        onRoundEnd.Action += IncreaseHealth;
        onGameOver.Action += OnGameOver;
    }

    private void OnDisable()
    {
        onNukeEvent.Action -= KillAll;
        onMonkeyStartEvent.Action -= TargetMonkey;
        onMonkeyEndEvent.Action -= RemoveMonkeyTarget;
        onPlayerDownedEvent.Action -= RemovePlayer;
        onPlayerRevivedEvent.Action -= AddPlayer;
        onRoundEnd.Action -= IncreaseHealth;
        onGameOver.Action -= OnGameOver;
    }

    private void Start()
    {

        InvokeRepeating(nameof(CalculatePaths), 0, timeBetweenPathUpdate);

    }

    private void CreateEnemies()
    {
        for (int i = 0; i < totalAMountToPool; i++)
        {
            //Instantiate(enemyPrefab, transform.position, transform.rotation);
            Enemy newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation, this.transform);
            newEnemy.Init(baseHealth);
            newEnemy.SetTarget(currentTarget);
            newEnemy.Inactivate();
            //newEnemy.gameObject.SetActive(false);
            enemies[i] = newEnemy;
        }
    }

    /// <summary>
    /// Return an inactive enemy, if no enemy is inactive returns null
    /// </summary>
    /// <returns>Enemy</returns>
    public Enemy GetInactiveEnemy()
    {
        for (int i = 0; i < enemies.Length; i++)
            if (!enemies[i].HealthSystem.IsAlive)
            {
                enemies[i].HealthSystem.SetCurrentMaxHealth(currentHealth); // should probs be dones somewhere else but brain hurty
                return enemies[i];
            }
        return null; // no enemy is active
    }

    private void Update()
    {
        //CalculatePaths();
    }
    private void CalculatePaths()
    {
        if (enemies == null || currentTarget == null)
        {
            //Debug.Log("enemeis or currentTarget null won't look for path");
            return;
        }
        foreach (Enemy enemy in enemies)
        {
            if (!enemy.HealthSystem.IsAlive) continue;
            enemy.SetPath(pathfinder.FindPath(enemy.transform.position, currentTarget.position));
        }
    }

    private void KillAll(int j)
    {
        for (int i = 0; i < enemies.Length; i++)
            // needs to be updated to see if enemy is alove not just active
            if (enemies[i].HealthSystem.IsAlive)
                enemies[i].Kill();
    }

    private void RemovePlayer()
    {
        //player = null;
        playerAlive = false;
        FindNextTarget();
    }

    private void AddPlayer(Transform player)
    {
        //this.player = player;
        playerAlive = true;
        FindNextTarget();
    }

    private void TargetMonkey(Transform target)
    {
        if (currentTarget == player)
        {

            currentTarget = target;
            SetEnemiesTarget();
        }
        int index = FindEmptySpotInTargets();
        if (index == -1)
            targets.Add(target);
        else
            targets[index] = target;
    }

    private void RemoveMonkeyTarget(Transform target)
    {
        targets.Remove(target);
        FindNextTarget();
    }

    private void FindNextTarget()
    {
        bool targetsEmpty = IsTargetsEmpty();
        if (targetsEmpty && playerAlive)
        {
            Debug.Log("Setting target to player");
            currentTarget = player;
            SetEnemiesTarget();
        }
        else if (targetsEmpty && !playerAlive)
        {
            Debug.Log("Setting target to null");
            currentTarget = null;
            SetEnemiesTarget();
        }
        else
        {
            int index = FindValidTarget();
            currentTarget = targets[index];
            Debug.Log("Setting target to targets");
            if (index == -1)
                currentTarget = player;
            SetEnemiesTarget();
        }
    }

    private bool IsTargetsEmpty()
    {
        foreach (Transform target in targets)
        {
            if (target != null)
                return false;
        }
        return true;
    }

    private int FindEmptySpotInTargets()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
                return i;
        }
        return -1;
    }

    private int FindValidTarget()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null)
                return i;
        }
        return -1;
    }


    /// <summary>
    /// Sets enemies target to currentTarget
    /// </summary>
    private void SetEnemiesTarget()
    {
        for (int i = 0; i < enemies.Length; i++)
            enemies[i].SetTarget(currentTarget);
    }

    private void IncreaseHealth(int currentRound)
    {
        if (currentRound < 10)
            currentHealth += 100;
        else
            currentHealth *= 10;
    }

    private void OnGameOver()
    {
        currentTarget = null;
        SetEnemiesTarget();
    }

}
