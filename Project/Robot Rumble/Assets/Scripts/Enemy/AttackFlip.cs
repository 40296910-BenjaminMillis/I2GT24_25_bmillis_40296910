using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFlip : AttackType
{
    [SerializeField] float launchForce;
    [SerializeField] Animator animator;

    public override void Attack(){
        base.Attack();
        if(attackCooldown > 0){
            attackCooldown -= Time.deltaTime;
        }
    }

    // Flip the sucker! Launch the player away and up into the air, to attempt to throw them off of the arena
    void OnTriggerStay(Collider other){
        if(attackCooldown <= 0 && other.tag == "Player" && enemyBehaviour.GetIsActive()){
            animator.SetTrigger("FlipTrigger");
            // The player needs their own script to handle the force applied to them
            other.GetComponent<ImpactReceiver>().AddImpact((transform.forward*1.5f) + Vector3.up, launchForce);
            attackCooldown = attackDelay;
        }
    }
}
