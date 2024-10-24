using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    ScoreManager scoreManager;
    [SerializeField] int pointsOnKill = 100;

    void Awake() {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public override void Die(){
        scoreManager.UpdateScore(pointsOnKill);
        base.Die();
    }
}
