using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFly : MoveRanged
{    
    [Header("Flight Settings")]
    [SerializeField] float hoverHeight = 10f; // The height the enemy tries to stay above the ground
    [SerializeField] float hoverStrength = 10f; // How much the enemy bounces back after getting below the hover height

    public override void Move(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position, new Vector3(0, -1, 0), out hit, hoverHeight)){ 
            enemyRb.velocity = new Vector3(0, hoverStrength, 0);
        }

       base.Move();
    }
}
