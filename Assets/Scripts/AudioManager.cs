using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;
    [Header("Audio Clip")]
    public AudioClip backgroundMusic;
    public AudioClip footstepSound;
    public AudioClip clickSound;
    public AudioClip tapSound;

    void Start() {
        instance = this;
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) {
        soundSource.PlayOneShot(clip);
    }

    public void PlayTapSound() {
        soundSource.PlayOneShot(tapSound);
    }
}
