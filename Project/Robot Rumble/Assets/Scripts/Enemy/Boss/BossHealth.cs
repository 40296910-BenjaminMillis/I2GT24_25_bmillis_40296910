using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : Health
{
    [SerializeField] ParticleSystem deathEffect;
    [SerializeField] int rank;
    ScoreManager scoreManager;
    AudioPlayer audioPlayer;
    int pointsOnKill;
    int scoreMultiplier = 1; // Certain actions, such as throw kills or killfloor kills grant score multiplier

    void Awake() {
        scoreManager = FindObjectOfType<ScoreManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        pointsOnKill = rank * 100; // Points granted based on rank of enemy
        FindObjectOfType<UIManager>().ToggleBossHealthbarOn(health);
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
        FindObjectOfType<UIManager>().ToggleBossHealthbarOff();
        CalculateScore();
        audioPlayer.PlayEnemyDeathClip(this.transform.position);
        ParticleSystem instance = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        base.Die();
    }
}
