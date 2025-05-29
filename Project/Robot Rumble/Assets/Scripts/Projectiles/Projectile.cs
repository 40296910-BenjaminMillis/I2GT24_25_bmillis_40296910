using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float projectileSpeed = 1f;
    [SerializeField] int projectileDamage = 1;
    [SerializeField] protected float projectileLifetime = 20f; // Time a projectile can exist before colliding with something

    protected Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        DifficultySettings difficultySettings = FindObjectOfType<DifficultySettings>();
        projectileSpeed *= difficultySettings.GetEnemySpeed();
    }
    
    void Update(){
        // Remove the projectile if it has existed as long as the set lifetime
        projectileLifetime -= Time.deltaTime;
        if(projectileLifetime <= 0){
            RemoveProjectile();
        }
        rb.velocity = transform.forward * projectileSpeed;
    }

    void OnTriggerEnter(Collider other) {
        // Checking for both players and enemies to allow for friendly fire
        if(other.CompareTag("Player")){ 
            Health targetHealth = other.GetComponent<Health>();
            targetHealth.UpdateHealth(-projectileDamage);
            RemoveProjectile();
        }
        else if(other.CompareTag("Enemy")){
            // Ignore the enemy and pass through them
        }
        else{
            RemoveProjectile();
        }
    }

    virtual protected void RemoveProjectile(){
        Destroy(gameObject);
    }
}
