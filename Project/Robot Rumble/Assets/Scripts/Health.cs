using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 3;

    bool isDead = false;

    // Pass in either positive or negative values to adjust health
    public virtual void UpdateHealth(int value){
        health += value;
        if(health <= 0 && !isDead){
            Die();
            Debug.Log("health: " + health);
        }
    }

    public virtual void Die(){
        isDead = true;
        Destroy(gameObject);
    }

    public int GetHealth(){
        return health;
    }
}
