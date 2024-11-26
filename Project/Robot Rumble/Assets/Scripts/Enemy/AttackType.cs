using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackType : MonoBehaviour
{
    [SerializeField] protected float attackDelay; // The amount of time it takes between attacks

    protected float attackCooldown; // The countdown of attackDelay
    protected Transform playerTransform;
    protected AudioPlayer audioPlayer;

    void Start(){
        attackCooldown = Random.Range(attackDelay, attackDelay*1.5f); // Randomise the cooldown on start to make enemies not attack at the same time
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    public virtual void Attack(){
        if(playerTransform == null){ // If the player is not in the current scene, stop attacking
            return;
        }
    }
}
