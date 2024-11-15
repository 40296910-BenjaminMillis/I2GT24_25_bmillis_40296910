using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRanged : MoveType
{
    [SerializeField] float runAwayDistance = 15f; // How close the player can get before the enemy starts running away
    [SerializeField] float followDistance = 30f; // How far they player is when they enemy starts following

    public override void Move(){
        StartCoroutine(RotateTowardsPlayer());

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        Vector3 lookDirection = (playerTransform.position - transform.position).normalized;

        // Run away from player if they get too close
        if(distance <= runAwayDistance){
            gameObject.GetComponent<Rigidbody>().AddForce(lookDirection * -moveSpeed * Time.deltaTime);
        }

        // Run towards from player if they get too far
        else if(distance > followDistance){
            gameObject.GetComponent<Rigidbody>().AddForce(lookDirection * moveSpeed * Time.deltaTime);
        }

        // Stop moving
        else{
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, gameObject.GetComponent<Rigidbody>().velocity.y, 0);
        }
    }
}
