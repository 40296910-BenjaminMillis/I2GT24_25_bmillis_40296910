using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] int rank = 1; //Determines the scoring of the enemy, from actual score to how they are weighted in spawning

    [Header("Projectile Stats")]
    [SerializeField] GameObject projectile;
    [SerializeField] float shotDelay = 5;

    [Header("Locations")]
    Transform playerTransform;
    [SerializeField] Transform firePosition;

    float shotCooldown;

    void Start(){
        shotCooldown = shotDelay;
        GameObject player = GameObject.Find("Player");
        playerTransform = player.transform;
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

    public int GetRank(){
        return rank;
    }
}
