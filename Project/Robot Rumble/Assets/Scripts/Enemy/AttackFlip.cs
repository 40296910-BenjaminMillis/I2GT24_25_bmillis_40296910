using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFlip : AttackType
{
    [SerializeField] float launchForce;

     public override void Attack(){
        base.Attack();
        if(attackCooldown > 0){
            attackCooldown -= Time.deltaTime;
        }
    }


    void OnTriggerStay(Collider other){
        //flip the sucker
        //do we need to check the type? YES, because player does not use rigidbody

        if(attackCooldown <= 0){
            //Vector3 awayFromAttacker = other.transform.position - transform.position;
            if(other.tag == "Player"){
                // The player needs their own script to handle the force applied to them
                other.GetComponent<ImpactReceiver>().AddImpact(transform.forward + (Vector3.up*2), launchForce);
            }
            

            if(other.tag == "Enemy"){
                other.GetComponent<Rigidbody>().AddForce((transform.forward + (Vector3.up*2)) * launchForce, ForceMode.Impulse);
                StartCoroutine(other.GetComponent<EnemyBehaviour>().MakeProne());
            }
            attackCooldown = attackDelay;
        }
    }
}
