using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Dash")]
    [SerializeField] AudioClip dashWooshClip;
    [SerializeField] [Range(0f, 1f)] float dashWooshVolume = 1f;
    [SerializeField] AudioClip dashHitClip;
    [SerializeField] [Range(0f, 1f)] float dashHitVolume = 1f;

    [Header("Explosion")]
    [SerializeField] AudioClip explosionClip;
    [SerializeField] [Range(0f, 1f)] float explosionVolume = 1f;

    [Header("Player Damage")]
    [SerializeField] AudioClip playerDamageClip;
    [SerializeField] [Range(0f, 1f)] float playerDamageVolume = 1f;

    static AudioPlayer instance;

    private void Awake() {
        instance = this;
    }

    public void PlayShootingClip(){
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayExplosionClip(){
        PlayClip(explosionClip, explosionVolume);
    }

    public void PlayPlayerDamageClip(){
        PlayClip(playerDamageClip, playerDamageVolume);
    }

    void PlayClip(AudioClip clip, float volume){
        if(clip != null){
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }

    public void PlayDashWooshClip(){
        PlayClip(dashWooshClip, dashWooshVolume);
    }

    public void PlayDashHitClip(){
        PlayClip(dashHitClip, dashHitVolume);
    }
}
