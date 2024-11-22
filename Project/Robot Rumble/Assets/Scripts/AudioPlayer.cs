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

    [Header("Enemy Projectile")]
    [SerializeField] AudioClip enemyProjectileClip;
    [SerializeField] [Range(0f, 1f)] float enemyProjectileVolume = 1f;

    [Header("Flames")]
    [SerializeField] AudioClip flamesClip;
    [SerializeField] [Range(0f, 1f)] float flamesVolume = 1f;

    float volumeLevel = 1;
    static AudioPlayer instance;

    private void Awake() {
        instance = this;
    }

    public void SetVolumeLevel(float volumeLevel){
        this.volumeLevel = volumeLevel;
    }

    void PlayClip(AudioClip clip, float volume, Vector3 position){
        if(clip != null){
            AudioSource.PlayClipAtPoint(clip, position, volume * volumeLevel);
        }
    }

    public void PlayShootingClip(Vector3 position){
        PlayClip(shootingClip, shootingVolume, position);
    }

    public void PlayExplosionClip(Vector3 position){
        PlayClip(explosionClip, explosionVolume, position);
    }

    public void PlayPlayerDamageClip(Vector3 position){
        PlayClip(playerDamageClip, playerDamageVolume, position);
    }

    public void PlayDashWooshClip(Vector3 position){
        PlayClip(dashWooshClip, dashWooshVolume, position);
    }

    public void PlayDashHitClip(Vector3 position){
        PlayClip(dashHitClip, dashHitVolume, position);
    }

    public void PlayEnemyProjectileClip(Vector3 position){
        PlayClip(enemyProjectileClip, enemyProjectileVolume, position);
    }

    public void PlayFlamesClip(Vector3 position){
        PlayClip(flamesClip, flamesVolume, position);
    }
}
