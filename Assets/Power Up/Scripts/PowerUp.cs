using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PowerUp : MonoBehaviour
{
    [SerializeField] private string playerTag;
    [SerializeField] private PowerUpType type;
    private bool activated;
    private PowerUpAudioManager audioManager;

    public PowerUpType Type {  get { return type; } }

    public enum PowerUpType
    {
        MaxAmmo,
        InstaKill,
        DoublePoints,
        Nuke,
        FireSale,
        Carpenter
    }

    private void Start()
    {
        //GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().isTrigger = true;
        audioManager = GetComponent<PowerUpAudioManager>();
    }

    public void Initiate(Vector3 position)
    {
        transform.position = position;
        audioManager.PlaySpawnSound();
        audioManager.StartLoop();
    }


    protected virtual void ActivatePowerUp()
    {
        Debug.Log(this + " activate");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(playerTag) && !activated)
        {
            ActivatePowerUp();
            audioManager.PlayPickupSound(transform.position);
            transform.gameObject.SetActive(false);
        }
    }
}
