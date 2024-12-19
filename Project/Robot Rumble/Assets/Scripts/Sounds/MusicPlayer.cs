using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip menuMusic;
    AudioSource audioSource;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    public void SetVolumeLevel(float volumeLevel){
        audioSource.volume = volumeLevel;
    }

    public void SetGameMusic(){
        audioSource.clip = gameMusic;
        audioSource.Play();
    }

    public void SetMenuMusic(){
        audioSource.clip = menuMusic;
        audioSource.Play();
    }
}
