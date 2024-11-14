using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveType : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 1000f;
    [SerializeField] protected float turnSpeed = 100f;

    protected Transform playerTransform;

    void Start(){
        playerTransform = FindObjectOfType<PlayerControl>().transform;
    }

    public virtual void Move(){
        // Need to check player position
        // Going to have 3 of these:
            // Persuit, to follow the player
            // Ranged, to run away from the player if they get too close
            // Flight, hover at a set height above the ground
    }
}
