using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform firePosition;
    [SerializeField] GameObject projectile;
    [SerializeField] float shotDelay = 5;

    float shotCooldown;

    void Start(){
        shotCooldown = shotDelay;
    }
    
    void Update(){
        Aiming();    
    }

    void Aiming(){
        transform.LookAt(playerTransform.position);
        if(shotCooldown <= 0){
            Shoot();
            shotCooldown = shotDelay;
        }
        else{
            shotCooldown -= Time.deltaTime;
        }
    }

    void Shoot(){
        // Create instance of projectile, which will fly in the direction the enemy was facing
        Instantiate(projectile, firePosition.position, transform.rotation);
    }
}
