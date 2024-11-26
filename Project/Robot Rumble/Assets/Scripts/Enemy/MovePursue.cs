using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePursue : MoveType
{
    // Move towards the player based on moveSpeed
    // Turn to face the player based on turnSpeed
    public override void Move(){
        if(playerTransform == null){
            return;
        }
        StartCoroutine(RotateTowardsPlayer());
        Vector3 lookDirection = (
            new Vector3(playerTransform.position.x, 0, playerTransform.position.z) 
            - new Vector3(transform.position.x, 0, transform.position.z)).normalized;
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // Run towards the player
        if(distance > 5){
            enemyRb.AddForce(lookDirection * moveSpeed * Time.deltaTime);
        }
        // Stop before getting too close (to be not on top of the player)
        else{
            enemyRb.velocity = new Vector3(0, enemyRb.velocity.y, 0);
        }
    }

    protected override IEnumerator RotateTowardsPlayer(){
        // Slight difference to the abstract method, that this method does not target the y position
        Quaternion targetRotation = Quaternion.LookRotation(
            new Vector3(playerTransform.position.x, 0, playerTransform.position.z) 
            - new Vector3(transform.position.x, 0, transform.position.z));
        float rotationTime = 0f;
        while (rotationTime < turnDelay)
        {
            // Rotate towards the player with a set delay
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationTime / turnDelay);
            yield return null;
            rotationTime += Time.deltaTime;
        }
    }
}
