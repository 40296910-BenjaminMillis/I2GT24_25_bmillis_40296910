using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] int pointsOnKill = 100;
    ScoreManager scoreManager;
    WaveManager waveManager;

    void Awake() {
        scoreManager = FindObjectOfType<ScoreManager>();
        waveManager = FindObjectOfType<WaveManager>();
    }

    public override void Die(){
        scoreManager.UpdateScore(pointsOnKill);
        waveManager.UpdateEnemyCount(-1);
        base.Die();
    }
}
