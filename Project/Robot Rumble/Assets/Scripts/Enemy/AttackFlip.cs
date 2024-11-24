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
        // Flip the sucker!
        
        // I will need to change this, as this conflicts with the ontrigger of enemybehaviour
        // I will probably make new scripts for that for each enemy type

        if(attackCooldown <= 0){
            if(other.tag == "Player"){
                // The player needs their own script to handle the force applied to them
                other.GetComponent<ImpactReceiver>().AddImpact((transform.forward*1.5f) + Vector3.up, launchForce);
            }
            
            // Disabled for now, it sort of works but has weird interactions with prone enemies
            // if(other.tag == "Enemy"){
            //     other.GetComponent<Rigidbody>().AddForce((transform.forward + (Vector3.up*2)) * launchForce, ForceMode.Impulse);
            //     StartCoroutine(other.GetComponent<EnemyBehaviour>().MakeProne());
            // }
            attackCooldown = attackDelay;
        }
    }
}
