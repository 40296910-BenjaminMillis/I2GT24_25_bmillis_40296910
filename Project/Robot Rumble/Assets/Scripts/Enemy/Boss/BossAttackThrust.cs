using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackThrust : BossAttack
{
    [Header("Thrust")]
    [SerializeField] float thrustSpeed = 2000;
    [SerializeField] float thrustDistance = 120; // Based on the start position, how far the thrust can travel
    Rigidbody rb;

    void Start(){
        attackCooldown = attackDelay;
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        audioPlayer = FindObjectOfType<AudioPlayer>();
        rb = GetComponent<Rigidbody>();
        startPosition = transform.localPosition;
    }

    private void Update(){
        if(isAttacking){
            transform.rotation = Quaternion.Euler(-90, 0, 0);
            Attack();
        }
        else{
            //idle stuff
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public override void Attack(){
        base.Attack();

        if(!finishingAttack){
            if(attackCooldown > 0){
                attackCooldown -= Time.deltaTime;
                // Hover to the side of the player to aim, until 1 second remains
                if(attackCooldown > stopAimingTime){
                    transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y-1, aimPositionOffset.z);
                    warningZone.enabled = true;
                }
            }

            else if(attackCooldown <= 0){
                // Move towards targeted position, until reaching set thrust distance
                warningZone.enabled = false;
                if(transform.localPosition.z >= startPosition.z + thrustDistance){
                    rb.velocity = Vector3.zero;
                    StartCoroutine(HitStop());
                }
                else{
                    rb.AddForce(Vector3.back * thrustSpeed * Time.deltaTime);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(attackCooldown <= 0){
            if(other.GetComponent<Health>()){
                other.GetComponent<Health>().UpdateHealth(-1);
            }
        }
    }
}
