using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] protected IntIntEventSO onReloadEvent;
    [SerializeField] protected EventSO onMaxAmmoEvent;


    [Header("Info")]
    [SerializeField] protected string gunName;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    [Header("Juice")]
    [SerializeField] private float muzzleFlashTime;

    [Header("Ammo")]
    [SerializeField] protected int magAmmo;
    [SerializeField] protected int maxMagAmmo;
    [SerializeField] protected int stockpileAmmo;
    [SerializeField] protected int maxStockpileAmmo;

    [Header("Damage")]
    [SerializeField] protected float damage; //per bullet
    [SerializeField] protected float headShotMultiplier;

    [Header("Attributes")]
    [SerializeField] protected Type gunType;
    [SerializeField] protected float bulletVelocity;
    [SerializeField] protected float range;
    [SerializeField] protected bool triggerFire;

    [SerializeField] protected float fireRate; //bullets per second 
    protected float secondsPerShot;
    protected bool isFiring;
    protected bool isReloading;

    [SerializeField] protected float reloadTime;
    [SerializeField] protected Transform firePoint;



    private bool triggerHasBeenReleased;

    private WeaponHandler weaponHandler;
    protected DamageInfo damageInfo;
    private MuzzleFlashes flashes;
    protected WeaponAudioManager audioPlayer;
    //private SpriteRenderer spriteRenderer;

    public enum Type
    {
        Pistol,
        AssultRifle,
        SubMachineGun,
        LightMachineGun,
        Sniper,
        Shotgun,
        WonderWeapon
    }

    public Type GunType { get { return gunType; } }

    protected bool CanFire
    {
        get { return !isFiring && !isReloading && magAmmo > 0; }
    }
    public bool IsUpgraded { get; set; }

    public string GunName { get { return gunName; } }
    public int CurrentMagAmmo { get { return magAmmo; } }
    public int StockpileAmmo { get { return stockpileAmmo; } }

    public Sprite Sprite
    {
        get
        {
            return spriteRenderer.sprite;
        }
    }

    protected virtual void OnEnable()
    {
        onMaxAmmoEvent.Action += FillAmmo;
    }

    protected virtual void OnDisable()
    {
        onMaxAmmoEvent.Action -= FillAmmo;
    }

    private void Awake()
    {
    }

    protected virtual void Start()
    {
        flashes = GetComponentInChildren<MuzzleFlashes>();
        audioPlayer = GetComponent<WeaponAudioManager>();
    }

    public virtual void StartReload()
    {
        if (!isReloading && stockpileAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }
    protected virtual IEnumerator Reload()
    {
        isReloading = true;
        audioPlayer.PlayReloadSound();

        if (weaponHandler)
            yield return new WaitForSeconds(reloadTime * weaponHandler.ReloadSpeedMultiplier);
        else
        {
            Debug.LogWarning("WeaponHandler is null ReloadSpeedMultiplier won't be applied");
            yield return new WaitForSeconds(reloadTime * weaponHandler.ReloadSpeedMultiplier);
        }


        if (stockpileAmmo >= maxMagAmmo)
        {
            int ammoLeftInMag = maxMagAmmo - magAmmo;
            if (ammoLeftInMag == 0) //fill mag and remove the maxMagAmmo from remaining ammo
            {
                magAmmo = maxMagAmmo;
                stockpileAmmo -= maxMagAmmo;
            }
            else // fill mag and remove maxMagAmmo - leftinmag from remaining ammo
            {
                magAmmo = maxMagAmmo;
                //Debug.Log("ammoLeftInMag " + ammoLeftInMag);
                stockpileAmmo -= ammoLeftInMag;
            }
        }
        else
        {
            //results in overloading ammo
            magAmmo += stockpileAmmo;
            stockpileAmmo -= stockpileAmmo;
        }

        onReloadEvent.Invoke(magAmmo, stockpileAmmo);
        isReloading = false;
        isFiring = false;
    }

    public void CancelReload()
    {
        StopCoroutine(Reload());
        isReloading = false;
        isFiring = false;
    }

    public virtual void Shoot(DamageInfo damageInfo)
    {
        if (CanFire)
        {
            StartCoroutine(FireBullet(damageInfo));
            if (flashes != null)
                flashes.ShowMuzzleFlash(muzzleFlashTime);
            if (weaponHandler.ExtraShot)
            {
                FireExtraBullet(damageInfo);
                //Debug.Log("Extra bullet fired");
            }
            onReloadEvent.Invoke(magAmmo, stockpileAmmo);
        }
    }

    public void TriggerDown(DamageInfo damageInfo)
    {
        if (triggerFire)
        {
            if (triggerHasBeenReleased)
            {
                Shoot(damageInfo);
                triggerHasBeenReleased = false;
            }
        }
        else
        {
            Shoot(damageInfo);
        }


    }

    public void TriggerReleased()
    {
        triggerHasBeenReleased = true;
    }

    protected virtual IEnumerator FireBullet(DamageInfo damageInfo)
    {
        audioPlayer.PlayShootSound();
        yield return new WaitForSeconds(0); // we dont want to do anything here really
    }

    /// <summary>
    /// Returns direction to mouse from firingPoint
    /// </summary>
    /// <returns></returns>
    protected Vector2 GetFiringDirection()
    {
        return transform.right;
        //return HelperFunctions.GetDirToMouse(firePoint.position);
    }

    /// <summary>
    /// Uses FiringDir to calculate witch way a bullet will fly taking into accound recoil, accuracy etc
    /// 
    /// NOT FULLY IMPLEMENTED
    /// 
    /// </summary>
    /// <returns></returns>
    protected virtual Vector2 CalculateBulletDir()
    {
        return GetFiringDirection();
    }

    public virtual void FillAmmo()
    {
        stockpileAmmo = maxStockpileAmmo;
        onReloadEvent.Invoke(magAmmo, stockpileAmmo);
    }

    public virtual void Init(WeaponHandler weaponHandler)
    {
        this.weaponHandler = weaponHandler;
        damageInfo = weaponHandler.DamageInfo;

        secondsPerShot = 1 / fireRate;
        isFiring = false;
        isReloading = false;
    }



    protected virtual void FireExtraBullet(DamageInfo damageInfo)
    {
        audioPlayer.PlayShootSound();
    }
}
