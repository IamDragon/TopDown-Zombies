using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeHandler : MonoBehaviour
{
    [SerializeField] private Projectile Grenade;
    [SerializeField] private int grenades;
    [SerializeField] private int maxGrenades;
    [SerializeField] private Transform spawnPoint;

    [Header("Events")]
    [SerializeField] private SpriteEventSO onGrenadeFillEvent;
    [SerializeField] private SpriteEventSO onGrenadeInecreaseEvent;
    [SerializeField] private EventSO onGrenadeDecreaseEvent;
    [SerializeField] private EventSO onMaxAmmo;

    private void Start()
    {
        for (int i = 0; i < maxGrenades; i++)
            onGrenadeInecreaseEvent.Invoke(Grenade.Sprite);
        FillGrenades();
    }

    private void OnEnable()
    {
        onMaxAmmo.Action += FillGrenades;
    }

    private void OnDisable()
    {
        onMaxAmmo.Action -= FillGrenades;
    }

    public void ThrowGrenade(Vector2 direction, float grenadeSpeed)
    {
        if (grenades <= 0)
            return;

        grenades--;
        Projectile grenade = Instantiate(Grenade, spawnPoint.position, transform.rotation);

        grenade.GetComponent<Movement>().SetVelocity(direction, grenadeSpeed);
        onGrenadeDecreaseEvent.Invoke();
    }

    //private void AddGrenade()
    //{
    //    grenades++;
    //    OnGrenadeInecrease?.Invoke(Grenade.Sprite);
    //}

    private void FillGrenades()
    {
        grenades = maxGrenades;
        onGrenadeFillEvent.Invoke(Grenade.Sprite);
    }

    public void RecieveNewGrenadeType(Projectile grenade)
    {
        this.Grenade = grenade;
        FillGrenades();
    }
}
