using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------Audio Source--------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------Audio Clip--------")]
    public AudioClip bombExplode;
    public AudioClip cellReveal;
    public AudioClip flag;
    public AudioClip flood;
    public AudioClip menuClick;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
