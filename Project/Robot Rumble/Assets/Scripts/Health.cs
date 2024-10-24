using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 3;

    //Pass in either positive or negative values to 
    public void UpdateHealth(int value){
        health += value;
        if(health <= 0){
            Die();
        }
        Debug.Log("health: " + health);
    }

    public virtual void Die(){
        Destroy(gameObject);
    }

    public float GetHealth(){
        return health;
    }
}
