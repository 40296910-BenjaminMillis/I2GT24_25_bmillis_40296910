using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePursue  : MoveType
{
    // Move towards the player based on moveSpeed
    // Turn to face the player based on turnSpeed
    public override void Move(){
        base.Move();
        Vector3 lookDirection = (playerTransform.position - transform.position).normalized;
        StartCoroutine(RotateTowardsPlayer());
        enemyRb.AddForce(lookDirection * moveSpeed * Time.deltaTime);
    }
}
