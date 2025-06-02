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

    [Header("Enemy")]
    [SerializeField] AudioClip enemyProjectileClip;
    [SerializeField] [Range(0f, 1f)] float enemyProjectileVolume = 1f;
    [SerializeField] AudioClip enemyFlipClip;
    [SerializeField] [Range(0f, 1f)] float enemyFlipVolume = 1f;
    [SerializeField] AudioClip enemyDeathClip;
    [SerializeField] [Range(0f, 1f)] float enemyDeathVolume = 1f;
    [SerializeField] AudioClip enemyKillfloorClip;
    [SerializeField] [Range(0f, 1f)] float enemyKillfloorVolume = 1f;

    [Header("Boss")]
    [SerializeField] AudioClip bossLaughClip;
    [SerializeField] [Range(0f, 1f)] float bossLaughVolume = 1f;
    [SerializeField] AudioClip bossChargeClip;
    [SerializeField] [Range(0f, 1f)] float bossChargeVolume = 1f;
    [SerializeField] AudioClip bossThudClip;
    [SerializeField] [Range(0f, 1f)] float bossThudVolume = 1f;
    [SerializeField] AudioClip bossThrustClip;
    [SerializeField] [Range(0f, 1f)] float bossThrustVolume = 1f;
    [SerializeField] AudioClip bossDeathClip;
    [SerializeField] [Range(0f, 1f)] float bossDeathVolume = 1f;

    [Header("Flames")]
    [SerializeField] AudioClip flamesClip;
    [SerializeField] [Range(0f, 1f)] float flamesVolume = 1f;

    [Header("Wave Sounds")]
    [SerializeField] AudioClip waveEndClip;
    [SerializeField] [Range(0f, 1f)] float waveEndVolume = 1f;

    [Header("Powerup")]
    [SerializeField] AudioClip powerupFallClip;
    [SerializeField] [Range(0f, 1f)] float powerupFallVolume = 1f;
    [SerializeField] AudioClip powerupCollectClip;
    [SerializeField] [Range(0f, 1f)] float powerupCollectVolume = 1f;

    float volumeLevel = 1;
    static AudioPlayer instance;
    AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
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

    public void PlayEnemyFlipClip(Vector3 position){
        PlayClip(enemyFlipClip, enemyFlipVolume, position);
    }

    public void PlayEnemyDeathClip(Vector3 position){
        PlayClip(enemyDeathClip, enemyDeathVolume, position);
    }

    public void PlayEnemyKillfloorClip(Vector3 position){
        PlayClip(enemyKillfloorClip, enemyKillfloorVolume, position);
    }

    public void PlayBossLaughClip(Vector3 position){
        audioSource.pitch = 1;
        audioSource.PlayOneShot(bossLaughClip, bossLaughVolume);
    }

    public void PlayBossChargeClip(){
        audioSource.pitch = 1 * FindObjectOfType<DifficultySettings>().GetEnemySpeed();
        audioSource.PlayOneShot(bossChargeClip, bossChargeVolume);
    }

    public void PlayBossThudClip(Vector3 position){
        PlayClip(bossThudClip, bossThudVolume, position);
    }

    public void PlayBossThrustClip(Vector3 position){
        PlayClip(bossThrustClip, bossThrustVolume, position);
    }

    public void PlayBossDeathClip(Vector3 position){
        PlayClip(bossDeathClip, bossDeathVolume, position);
    }

    public void PlayFlamesClip(Vector3 position){
        PlayClip(flamesClip, flamesVolume, position);
    }

    public void PlayWaveEnd(Vector3 position){
        PlayClip(waveEndClip, waveEndVolume, position);
    }

    public void PlayPowerupFallClip(Vector3 position){
        PlayClip(powerupFallClip, powerupFallVolume, position);
    }

    public void PlayPowerupCollectClip(Vector3 position){
        PlayClip(powerupCollectClip, powerupCollectVolume, position);
    }
}
