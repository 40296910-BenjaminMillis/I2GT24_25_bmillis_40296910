using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] int rank = 1; //Determines the scoring of the enemy, from actual score to how they are weighted in spawning
    [SerializeField] float gravity = 10f;

    [Header("Projectile Stats")]
    [SerializeField] GameObject projectile;
    [SerializeField] float shotDelay = 5;

    [Header("Locations")]
    Transform playerTransform;
    [SerializeField] Transform firePosition;

    float shotCooldown;
    Rigidbody rb;
    bool isActive = true;

    void Awake(){
        shotCooldown = shotDelay;
        PlayerControl player = FindObjectOfType<PlayerControl>();
        playerTransform = player.transform;
        rb = GetComponent<Rigidbody>();
    }
    
    void Update(){
        if(isActive){
            Aiming();
        }
        rb.AddForce(Physics.gravity * gravity * Time.deltaTime, ForceMode.Acceleration);
    }

    void Aiming(){
        if(playerTransform != null){
            transform.LookAt(playerTransform.position + Vector3.up);
            if(shotCooldown <= 0){
                Shoot();
                shotCooldown = shotDelay;
            }
            else{
                shotCooldown -= Time.deltaTime;
            }
        }
    }

    void Shoot(){
        // Create instance of projectile, which will fly in the direction the enemy was facing
        Instantiate(projectile, firePosition.position, transform.rotation);
    }

    public void SetIsActive(bool isActive){
        this.isActive = isActive;
    }

    public int GetRank(){
        return rank;
    }
}
