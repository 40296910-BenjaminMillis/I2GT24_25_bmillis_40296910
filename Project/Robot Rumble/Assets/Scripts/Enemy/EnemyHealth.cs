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
    int scoreMultiplier = 1;

    void Awake() {
        scoreManager = FindObjectOfType<ScoreManager>();
        waveManager = FindObjectOfType<WaveManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        rank = GetComponent<EnemyBehaviour>().GetRank();
        pointsOnKill = rank * 100;
    }

    public override void UpdateHealth(int value){
        scoreMultiplier = 1;
        base.UpdateHealth(value);
    }

    public override void UpdateHealth(int value, int scoreMultiplier){
        this.scoreMultiplier = scoreMultiplier;
        base.UpdateHealth(value);
    }

    void CalculateScore(){
        scoreManager.UpdateScore(pointsOnKill * scoreMultiplier);
    }

    public override void Die(){
        CalculateScore();
        waveManager.UpdateEnemyCount(-rank);
        audioPlayer.PlayExplosionClip(this.transform.position);
        ParticleSystem instance = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        base.Die();
    }
}
