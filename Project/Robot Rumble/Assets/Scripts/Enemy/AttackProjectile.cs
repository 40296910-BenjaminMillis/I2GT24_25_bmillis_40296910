using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : AttackType
{
    [Header("Projectile Settings")]
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject firePosition;

    public override void Attack(){
        base.Attack();
        Aiming();
    }

    void Aiming(){
        if(attackCooldown <= 0){
            Shoot();
            attackCooldown = attackDelay;
        }
        else{
            attackCooldown -= Time.deltaTime;
        }
    }

    void Shoot(){
        // Create instance of projectile, which will fly in the direction the enemy was facing
        Instantiate(projectile, firePosition.transform.position, transform.rotation);
    }
}
