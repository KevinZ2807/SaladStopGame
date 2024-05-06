using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;
    [Header("Audio Clip")]
    public AudioClip backgroundMusic;
    public AudioClip footstepSound;
    public AudioClip clickSound;
    public AudioClip tapSound;

    void Start() {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) {
        soundSource.PlayOneShot(clip);
    }
}
