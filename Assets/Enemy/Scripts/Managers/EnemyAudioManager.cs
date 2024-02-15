using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] enemyHurtClips;
    [SerializeField] private AudioClip[] enemyChaseClips;

    [Range(0, 100)]
    [SerializeField] private float percentToPlaySound;
    [SerializeField] private float interval;

    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating(nameof(PlayChaseSound), 0, interval);
    }

    public void PlayHurtSound()
    {
        audioSource.PlayOneShot(enemyHurtClips[Random.Range(0, enemyHurtClips.Length - 1)]);
    }

    public void PlayChaseSound()
    {
        int chance = Random.Range(0, 100);

        if (chance <= percentToPlaySound)
            audioSource.PlayOneShot(enemyChaseClips[Random.Range(0, enemyChaseClips.Length - 1)]);
    }
}
