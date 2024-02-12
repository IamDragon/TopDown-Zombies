using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [Header("Guns")]
    [SerializeField] private Gun[] startingGuns;
    [SerializeField] private Gun[] guns;
    //[SerializeField] private int activeGunIndex;
    [SerializeField] private Gun activeGun;
    private int activeGunIndex;
    private int lastGunIndex;

    [Header("DownedGuns")]
    [SerializeField] private Gun defaultDownedGun;
    [SerializeField] private Gun.Type validDownedGunType;


    public bool CanHoldExtraGun { get; set; }
    private DamageInfo damageInfo;
    private const int maxGuns = 3;

    [Header("Reload")]
    [SerializeField] private float baseReloadSpeedMultiplier;
    [SerializeField] private float reloadSpeedMultiplier;

    [Header("Events")]
    [SerializeField] private GunEventSO onSwitchGunEvent;
    [SerializeField] private IntIntEventSO onUpdateAmmoEvent;
    [SerializeField] protected EventSO onMaxAmmoEvent;


    private bool extraShot;
    public bool ExtraShot { get { return extraShot; } }
    public DamageInfo DamageInfo { get { return damageInfo; } }

    public float ReloadSpeedMultiplier { get { return reloadSpeedMultiplier; } set { reloadSpeedMultiplier = value; } }

    private void Start()
    {
        damageInfo = GetComponentInParent<PlayerVariables>().damageInfo;
        guns = new Gun[maxGuns];
        CreateStartingGuns();
        InactiveAllGuns();


        //activeGunIndex = 0;
        SwitchToGun(0);
        reloadSpeedMultiplier = baseReloadSpeedMultiplier;
    }

    private void OnEnable()
    {
        onMaxAmmoEvent.Action += FillAmmoForAllGuns;
    }

    private void OnDisable()
    {
        onMaxAmmoEvent.Action -= FillAmmoForAllGuns;
    }

    private void CreateStartingGuns()
    {
        if (startingGuns.Length < 0)
            Debug.LogWarning("no starting weapons will cause problems");

        for (int i = 0; i < startingGuns.Length; i++)
        {
            if (i >= maxGuns)
            {
                Debug.LogWarning("More guns in startingGuns than maxGuns allows; breaking");
                break;
            }
            Gun newGun = CreateGun(startingGuns[i]);
            guns[i] = newGun;
        }
    }

    private Gun CreateGun(Gun gun)
    {
        Gun newGun = Instantiate(gun, transform.position, transform.rotation, this.transform);
        newGun.Init(this);
        return newGun;
    }

    private void InactiveAllGuns()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i] != null)
                guns[i].gameObject.SetActive(false);
        }
        defaultDownedGun.gameObject.SetActive(false);
    }

    public void PullGunTrigger()
    {
        if (activeGun != null)
            activeGun.TriggerDown(damageInfo);
    }

    public void ReleaseGunTrigger()
    {
        if (activeGun != null)
            activeGun.TriggerReleased();
    }

    public void ReloadCurrentGun()
    {
        if (activeGun != null)
            activeGun.StartReload();
    }

    private void UnequipActiveGun()
    {
        if (activeGun != null)
        {
            activeGun.CancelReload();
            activeGun.gameObject.SetActive(false);
            activeGun = null;
            onUpdateAmmoEvent.Invoke(0, 0);
        }
    }

    /// <summary>
    /// Set gun at index to activeGun, if gun at that spot is null activeGun will be null
    /// </summary>
    /// <param name="index"></param>
    private void EquipGun(int index)
    {
        activeGun = guns[index];
        activeGun.gameObject.SetActive(true);
    }

    private void EquipGun(Gun gun)
    {
        activeGun = gun;
        gun.gameObject.SetActive(true);
    }

    public void SwitchToGun(int index)
    {
        if (guns[index] == null) return;
        UnequipActiveGun();

        //check if index is valid
        if (index >= guns.Length || index < 0)
        {
            Debug.LogWarning("gunIndex higher than number of guns in list or less than 0, setting activeIndex to first found gun");
            for (int i = 0; i < guns.Length; i++)
                if (guns[i] != null)
                {
                    activeGunIndex = i;
                    EquipGun(i);
                    break; // activate first found gun
                }
        }
        else if (guns[index] != null)
        {
            activeGunIndex = index;
            EquipGun(index);
        }

        if (activeGun != null)
            activeGun.gameObject.SetActive(true);
        else
            Debug.LogWarning("activeGun: " + activeGun + " is null");

        onSwitchGunEvent.Invoke(activeGun);
    }

    /// <summary>
    /// Adds new gun to index, if there already is a gun it is removed
    /// </summary>
    /// <param name="index"></param>
    /// <param name="newGun"></param>
    private void AddNewGun(int index, Gun newGun)
    {
        if (guns[index] != null)
        {
            guns[index].CancelReload();
            Destroy(guns[index].gameObject);
        }
        guns[index] = CreateGun(newGun);
        SwitchToGun(index);
    }

    /// <summary>
    /// Finds index to insert a new gun
    /// </summary>
    /// <returns>An index with with a spot for a gun, if none found returns -1</returns>
    private int FindIndexForGun()
    {
        int index = -1;
        for (int i = 0; i < guns.Length; i++)
        {
            if (i == guns.Length - 1 && !CanHoldExtraGun)
                continue;
            if (guns[i] == null)
            {
                //check if CanHoldExtragun
                index = i;
                break;
            }
        }
        if (index == -1)
            index = activeGunIndex;

        return index;

        //if (guns[0] == null) return 0;
        //else if (guns[1] == null) return 1;
        //else if (guns[2] == null && CanHoldExtraGun) { return 2; } //perk that gives another weapon slot

        //return -1;
    }

    /// <summary>
    /// Adds new gun to empty gun slot if none exists replaces active gun
    /// </summary>
    /// <param name="newGun"></param>
    public void ReceiveNewGun(Gun newGun)
    {
        int index = FindIndexForGun(); //method should return index of currentGun if none found
        //Debug.Log("Index to replace gun: " + index);
        if (index == -1)
            ReplaceCurrentGun(newGun, index);
        else
            AddNewGun(index, newGun);
    }

    /// <summary>
    /// Replaces active gun with newGun destroying the active gun gameobject
    /// </summary>
    /// <param name="newGun"></param>
    public void ReplaceCurrentGun(Gun newGun, int currentIndex)
    {

        if (activeGun != null)
        {
            activeGun.CancelReload(); //causes problems when picking up upgraded weapon
            //Destroy or inactivate weapon
            Destroy(activeGun.gameObject);
        }

        Gun gun = CreateGun(newGun);
        activeGun = gun;
    }

    /// <summary>
    /// Removes the active gun from the gun list and destroys the game object
    /// </summary>
    /// <returns>The gun that was removed</returns>
    public Gun RemoveActiveGun()
    {
        if (activeGun != null)
        {
            onUpdateAmmoEvent.Invoke(0, 0);
            activeGun.CancelReload();
            Destroy(activeGun.gameObject);
            Debug.Log(activeGun);
            Gun gunToReturn = activeGun;
            activeGun = null;
            return gunToReturn;
        }

        return null;
    }

    public void ResetReloadSpeedMultiplier()
    {
        reloadSpeedMultiplier = baseReloadSpeedMultiplier;
    }

    public void ActivateExtraShot()
    {
        extraShot = true;
    }

    public void DeactivateExtraShot()
    {
        extraShot = false;
    }

    public void SwitchToStandingGuns()
    {
        if (activeGun != null && activeGun != defaultDownedGun)
        {
            onUpdateAmmoEvent.Invoke(0, 0);
            activeGun.CancelReload();
            Destroy(activeGun.gameObject);
        }

        SwitchToGun(activeGunIndex);
    }

    public void SwitchToDownedGun()
    {
        //lastGunIndex = GetActiveGunIndex();
        UnequipActiveGun();

        Gun validGun = GetValidDownedGun();// get valid gun
        if (validGun != null) //if there is a valid gun create it
        {
            activeGun = CreateGun(validGun);
            EquipGun(activeGun);
        }

        if (activeGun == null) // if no valid gun equip base valid gun
        {
            Debug.Log("activeGun was null, setting to default downed gun");
            EquipGun(defaultDownedGun);
        }
    }

    private int GetActiveGunIndex()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (activeGun == guns[i])
                return i;
        }

        return -1;
    }

    private Gun GetValidDownedGun()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i] == null)
                continue;
            if (guns[i].GunType == validDownedGunType)
                return guns[i];

        }

        return null;
    }

    public bool CanUpgradeGun()
    {
        if (activeGun == null)
            return false;
        return activeGun.IsUpgraded;
    }

    public bool HasGun(Gun gun)
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i] == null) continue;

            if (guns[i].GunName == gun.GunName)
                return true;
        }
        return false;
    }

    private void FillAmmoForAllGuns()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i] == null)
                continue;
            guns[i].FillAmmo();

        }
        onUpdateAmmoEvent.Invoke(activeGun.CurrentMagAmmo, activeGun.StockpileAmmo);
    }

    public void FillAmmoForGun(Gun gun)
    {
        //loop through guns and FillAmmo on specific gun
        for (int i = 0; i < guns.Length; i++)
        {
            if (guns[i] == null)
                continue;

            if (gun.GunName == guns[i].GunName)
            {
                Debug.Log("filling ammo");
                guns[i].FillAmmo();
                break;
            }
        }
    }
}
