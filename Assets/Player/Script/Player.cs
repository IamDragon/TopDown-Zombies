using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static Player instance;

    WeaponHandler weaponHandler;
    PlayerStats playerStats;
    GrenadeHandler grenadeHandler;
    PointManager pointManager;
    PlayerHealthSystem playerHealthSystem;
    HitHandler hitHandler;
    PerkHandler perkHandler;
    PlayerAttackHandler playerAttackHandler;
    StateManager stateManager;
    PlayerVariables playerVariables;

    public static Player Instance { get { return instance; } }

    public WeaponHandler WeaponHandler { get { return weaponHandler; } }
    public PlayerStats PlayerStats { get { return playerStats; } }
    public GrenadeHandler GrenadeHandler { get { return grenadeHandler; } }
    public PointManager PointManager { get { return pointManager; } }
    public PlayerHealthSystem PlayerHealthSystem { get { return playerHealthSystem; } }
    public HitHandler HitHandler { get { return hitHandler; } }
    public PerkHandler PerkHandler { get { return perkHandler; } }
    public PlayerAttackHandler PlayerAttackHandler { get { return playerAttackHandler; } }
    public StateManager StateManager { get { return stateManager; } }
    public PlayerVariables PlayerVariables { get { return playerVariables; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        weaponHandler = GetComponentInChildren<WeaponHandler>();
        playerStats = GetComponent<PlayerStats>();
        grenadeHandler = GetComponent<GrenadeHandler>();
        pointManager = GetComponent<PointManager>();
        playerHealthSystem = GetComponent<PlayerHealthSystem>();
        hitHandler = GetComponent<HitHandler>();
        perkHandler = GetComponent<PerkHandler>();
        playerAttackHandler = GetComponent<PlayerAttackHandler>();
        stateManager = GetComponent<StateManager>();
        playerVariables= GetComponent<PlayerVariables>();
    }
}
