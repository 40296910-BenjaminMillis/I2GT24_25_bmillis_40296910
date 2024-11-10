using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] int pointsOnKill = 100;
    [SerializeField] ParticleSystem deathEffect;
    ScoreManager scoreManager;
    WaveManager waveManager;
    AudioPlayer audioPlayer;

    void Awake() {
        scoreManager = FindObjectOfType<ScoreManager>();
        waveManager = FindObjectOfType<WaveManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    public override void Die(){
        scoreManager.UpdateScore(pointsOnKill);
        waveManager.UpdateEnemyCount(-1);
        audioPlayer.PlayExplosionClip();
        ParticleSystem instance = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        base.Die();
    }
}
