using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : AttackType
{
    [Header("Projectile Settings")]
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject firePosition;

    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] float animationDelay; // Additional delay for the shot to wait for the animation to complete
    float animationCooldown;

    private void Awake() {
        animationCooldown = animationDelay;
    }

    public override void Attack(){
        base.Attack();
        Aiming();
    }

    void Aiming(){
        if(attackCooldown <= 0){
            // If an animation exists, add animation delay
            if(animator && animationCooldown > 0){
                animator.SetBool("Shoot", true);
                animationCooldown -= Time.deltaTime;
            }
            else{
                Shoot();
                attackCooldown = attackDelay;
                animationCooldown = animationDelay;
            }
        }
        else{
            attackCooldown -= Time.deltaTime;
        }
    }

    void Shoot(){

        // Create instance of projectile, which will fly in the direction the enemy was facing
        Instantiate(projectile, firePosition.transform.position, transform.rotation);
        audioPlayer.PlayEnemyProjectileClip(this.transform.position);
        if(animator)
            animator.SetBool("Shoot", false);
    }
}
