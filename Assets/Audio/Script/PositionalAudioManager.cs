using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionalAudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource prefab;
    [SerializeField] private int initialamount;
    private static PositionalAudioManager instance;
    List<AudioSource> audioSources;

    public static PositionalAudioManager Instance {  get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        audioSources = new List<AudioSource>();
        CreateAudioSources();
    }

    private void CreateAudioSources()
    {
        for (int i = 0; i < initialamount; i++)
        {
            CreateAudioSource();
        }
    }

    /// <summary>
    /// Creates AudioSource and places it in AudioSources
    /// </summary>
    /// <returns>Created AudioSource</returns>
    private AudioSource CreateAudioSource()
    {
        AudioSource source = Instantiate(prefab, transform.position, transform.rotation, this.transform);
        audioSources.Add(source);
        return source;
    }

    /// <summary>
    /// Plays audio in position on a available AudioSource
    /// </summary>
    /// <param name="position"></param>
    /// <param name="clip"></param>
    public void PlayAudio(Vector3 position, AudioClip clip)
    {
        AudioSource source = FindAvailableSource();
        source.transform.position = position;
        source.PlayOneShot(clip);
    }


    /// <summary>
    /// Finds an AudioSource thats not playing a sound if none is found creates one
    /// </summary>
    /// <returns>Found AudioSource or the created one</returns>
    private AudioSource FindAvailableSource()
    {
        for (int i = 0; i < audioSources.Count; i++)
        {
            if (!audioSources[i].isPlaying)
                return audioSources[i];
        }
        return CreateAudioSource();
    }
}
