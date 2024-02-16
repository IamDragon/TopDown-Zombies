using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAudioManager : MonoBehaviour
{
    //[SerializeField] private AudioClip powerupLoop; // dont have audio for loop
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private AudioClip spawnSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartLoop()
    {
        //audioSource.clip = powerupLoop;
        //audioSource.loop = true;
        //audioSource.Play();
    }

    public void StopLoop()
    {
        audioSource.loop = false;
        audioSource.Stop();
    }

    public void PlayPickupSound(Vector3 position)
    {
        //need to play on seperate audioSource as this will be inactivated when picked up
        PositionalAudioManager.Instance.PlayAudio(position, pickUpSound);
    }

    public void PlaySpawnSound()
    {
        audioSource.PlayOneShot(spawnSound);
    }

}
