using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip backingTrack;

    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource soundEffectsSource;
    [SerializeField] private AudioSource soundEffects2Source;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlaySoundTrack(backingTrack);
    }

    public void PlaySound(AudioClip clip)
    {
        soundEffectsSource.PlayOneShot(clip);
    }

    public void PlaySound2(AudioClip clip)
    {
        soundEffects2Source.PlayOneShot(clip);
    }

    public void PlaySoundTrack(AudioClip clip)
    {
        backgroundMusicSource.clip = clip;
        backgroundMusicSource.Play();
    }

    public void StopSoundTrack()
    {
        backgroundMusicSource.Stop();
    }
}
