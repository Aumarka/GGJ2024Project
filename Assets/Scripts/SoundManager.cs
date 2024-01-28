using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource soundEffectsSource;

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

    public void PlaySound(AudioClip clip)
    {
        soundEffectsSource.PlayOneShot(clip);
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
