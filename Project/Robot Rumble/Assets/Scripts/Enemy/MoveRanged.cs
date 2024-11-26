using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRanged : MoveType
{
    [Header("Ranged Settings")]
    [SerializeField] float runAwayDistance = 15f; // How close the player can get before the enemy starts running away
    [SerializeField] float followDistance = 30f; // How far they player is when they enemy starts following

    public override void Move(){
        if(playerTransform == null){ // If the player is not in the current scene, stop moving
            return;
        }
        StartCoroutine(RotateTowardsPlayer());

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        Vector3 lookDirection = (playerTransform.position - transform.position).normalized;

        // Run away from player if they get too close
        if(distance <= runAwayDistance){
            enemyRb.AddForce(lookDirection * -moveSpeed * Time.deltaTime);
        }

        // Run towards from player if they get too far
        else if(distance > followDistance){
            enemyRb.AddForce(lookDirection * moveSpeed * Time.deltaTime);
        }

        // Stop moving
        else{
            enemyRb.velocity = new Vector3(0, enemyRb.velocity.y, 0);
        }
    }
}
