using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 1f;
    [SerializeField] int projectileDamage = 1;

    Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update(){
        rb.velocity = transform.forward * projectileSpeed;
    }

    void OnTriggerEnter(Collider other) {
        // Checking for both players and enemies to allow for friendly fire
        if(other.tag == "Player" || other.tag == "Enemy"){ 
            Health targetHealth = other.GetComponent<Health>();
            targetHealth.UpdateHealth(-projectileDamage);
        }
        Destroy(gameObject);
    }
}
