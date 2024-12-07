using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttackSlam : BossAttack
{
    [Header("Slam")]
    [SerializeField] float slamSpeed = 2000;
    Rigidbody rb;

    void Start(){
        attackCooldown = attackDelay;
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        audioPlayer = FindObjectOfType<AudioPlayer>();
        rb = GetComponent<Rigidbody>();
        startPosition = transform.localPosition;
    }

    public override void Attack(){
        base.Attack();

        if(!finishingAttack){
            if(attackCooldown > 0){
                attackCooldown -= Time.deltaTime;
                // Hover above the player to aim, until 1 second remains
                if(attackCooldown > 1){
                    transform.position = playerTransform.position + aimPositionOffset;
                }
            }

            else if(attackCooldown <= 0){
                // Fall to the ground and attempt to squish the player
                rb.AddForce(Vector3.down * slamSpeed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(attackCooldown <= 0){
            if(other.GetComponent<Health>()){
                other.GetComponent<Health>().UpdateHealth(-1);
            }
            rb.velocity = Vector3.zero;
            StartCoroutine(HitStop());
        }
    }
}
