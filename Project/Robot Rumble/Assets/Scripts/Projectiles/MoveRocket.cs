using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRocket : MoveType
{
    // Move towards the player based on moveSpeed
    // Turn to face the player based on turnSpeed
    public override void Move(){
        if(playerTransform == null){
            return;
        }
        StartCoroutine(RotateTowardsPlayer());

        enemyRb.velocity = transform.forward*moveSpeed;
    }
}