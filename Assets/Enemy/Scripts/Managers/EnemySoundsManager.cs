using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundsManager : MonoBehaviour
{
    //every x seconds have y percent chance to play noise

    [Range(0,100)]
    [SerializeField] private float percentToPlaySound;
    [SerializeField] private float interval;

    private RandomAudioPlayer audioPlayer;


    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<RandomAudioPlayer>();
        InvokeRepeating(nameof(PlaySound), 0, interval);
    }

    private void PlaySound()
    {
        int chance = Random.Range(0, 100);

        if (chance <= percentToPlaySound)
            audioPlayer.PlayRandomAudio();
    }

}
