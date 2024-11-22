using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] ParticleSystem deathEffect;
    ScoreManager scoreManager;
    WaveManager waveManager;
    AudioPlayer audioPlayer;
    int rank;
    int pointsOnKill;

    void Awake() {
        scoreManager = FindObjectOfType<ScoreManager>();
        waveManager = FindObjectOfType<WaveManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        rank = GetComponent<EnemyBehaviour>().GetRank();
        pointsOnKill = rank * 100;
    }

    public override void Die(){
        scoreManager.UpdateScore(pointsOnKill);
        waveManager.UpdateEnemyCount(-rank);
        audioPlayer.PlayExplosionClip(this.transform.position);
        ParticleSystem instance = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        base.Die();
    }
}
