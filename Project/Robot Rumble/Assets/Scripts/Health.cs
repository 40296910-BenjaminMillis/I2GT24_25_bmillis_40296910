using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 3;
    [SerializeField] protected int health;
    bool isDead = false;

    void Start(){
        health = maxHealth;
    }

    // Pass in either positive or negative values to adjust health
    public virtual void UpdateHealth(int value){
        health += value;
        if(health <= 0 && !isDead){
            Die(); // Remove the gameobject if 0 health
        }
    }

    public virtual void UpdateHealth(int value, int scoreMultiplier){
    
    }

    public virtual void Die(){
        isDead = true;
        Destroy(gameObject);
    }

    public int GetHealth(){
        return health;
    }
}
