using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [Header("Powerups")]
    [SerializeField] private int spawnChance;
    /// <summary>
    /// Prefabs of each power up
    /// </summary>
    [SerializeField] private PowerUp[] prefabs;
    private List<PowerUp> powerups;


    [Header("Event")]
    [SerializeField] private EnemyHitHandlerEventSO onEnemyDeath;



    private void OnEnable()
    {
        onEnemyDeath.Action += DeterminePowerUpSpawn;
    }

    private void OnDisable()
    {
        onEnemyDeath.Action -= DeterminePowerUpSpawn;
    }

    private void Start()
    {
        powerups = new List<PowerUp>();
    }

    private void DeterminePowerUpSpawn(EnemyHitHandler enemyHitHandler)
    {
        if (!ShouldSpawn()) // not spawning power up
            return;
        PowerUp toSpawn = GetPowerUp();

        PowerUp powerup = FindAvailablePowerUp(toSpawn);
        if (powerup != null)
        {
            powerup.transform.position = enemyHitHandler.transform.position;
            powerup.gameObject.SetActive(true);
        }
        else
        {
            SpawnPowerUp(toSpawn, enemyHitHandler.transform.position);
        }
    }

    /// <summary>
    /// Spawns a new instance of a chosen power up
    /// </summary>
    /// <param name="toSpawn"></param>
    /// <param name="spawnPosition"></param>
    private void SpawnPowerUp(PowerUp toSpawn, Vector3 spawnPosition)
    {
        PowerUp powerup = Instantiate(GetPowerUp(), this.transform);
        powerup.transform.position = spawnPosition;
        powerups.Add(powerup);
    }

    /// <summary>
    /// Get the power up to spawn
    /// </summary>
    private PowerUp GetPowerUp()
    {
        return prefabs[Random.Range(0, prefabs.Length - 1)];
    }

    private bool ShouldSpawn()
    {
        if (spawnChance >= Random.Range(0, 101))
            return true;
        return false;
    }

    private PowerUp FindAvailablePowerUp(PowerUp toSpawn)
    {
        foreach (PowerUp powerup in powerups)
        {
            if (powerup.Type == toSpawn.Type && !powerup.isActiveAndEnabled) return powerup;
        }
        return null;
    }

}
