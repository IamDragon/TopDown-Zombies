using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{

    [SerializeField] private AudioClip[] clips;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomAudio()
    {
        int audioIndex = Random.Range(0, clips.Length - 1);
        audioSource.PlayOneShot(clips[audioIndex]);
    }
}
