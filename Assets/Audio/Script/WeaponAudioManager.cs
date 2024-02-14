using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] ShotClips;
    [SerializeField] private AudioClip[] relaodClips;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShootSound()
    {
        audioSource.PlayOneShot(ShotClips[Random.Range(0, ShotClips.Length - 1)]);
    }

    public void PlayReloadSound()
    {
        audioSource.PlayOneShot(relaodClips[Random.Range(0, relaodClips.Length - 1)]);
    }

}
