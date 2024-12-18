using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] GameObject deathEffect;
    ScoreManager scoreManager;
    WaveManager waveManager;
    AudioPlayer audioPlayer;
    int rank;
    int pointsOnKill;
    int scoreMultiplier = 1; // Certain actions, such as throw kills or killfloor kills grant score multiplier

    void Awake() {
        scoreManager = FindObjectOfType<ScoreManager>();
        waveManager = FindObjectOfType<WaveManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        rank = GetComponent<EnemyBehaviour>().GetRank();
        pointsOnKill = rank * 100; // Points granted based on rank of enemy
    }

    public override void UpdateHealth(int value){
        scoreMultiplier = 1;
        base.UpdateHealth(value);
    }

    // Only used if a score multiplier is being added
    public override void UpdateHealth(int value, int scoreMultiplier){ 
        this.scoreMultiplier = scoreMultiplier;
        base.UpdateHealth(value);
    }

    void CalculateScore(){
        scoreManager.UpdateScore(pointsOnKill * scoreMultiplier);
    }

    public override void Die(){
        CalculateScore();
        waveManager.UpdateEnemyCount(-rank); // Remove enemy from the waveManager count
        audioPlayer.PlayEnemyDeathClip(this.transform.position);
        GameObject instance = Instantiate(deathEffect, transform.position, Quaternion.identity);
        
        if(instance.GetComponent<ParticleSystem>())
            Destroy(instance.gameObject, instance.GetComponent<ParticleSystem>().main.duration + instance.GetComponent<ParticleSystem>().main.startLifetime.constantMax);
        
        base.Die();
    }
}
