using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnEnter : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float playCooldown;
    bool canPlay;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canPlay = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.Instance.tag))
            PlayAudio();
    }

    private void PlayAudio()
    {
        if (!audioSource.isPlaying && canPlay)
        {
            audioSource.PlayOneShot(audioClip);
            Invoke(nameof(AllowPlay), playCooldown + audioClip.length);
        }
    }

    private void AllowPlay()
    {
        canPlay = true;
    }

}
