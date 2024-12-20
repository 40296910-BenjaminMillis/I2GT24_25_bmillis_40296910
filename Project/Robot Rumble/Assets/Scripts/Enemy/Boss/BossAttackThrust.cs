using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackThrust : BossAttack
{
    [Header("Thrust")]
    [SerializeField] float thrustSpeed = 2000;
    [SerializeField] float thrustDistance = 120; // Based on the start position, how far the thrust can travel
    Rigidbody rb;
    float sfxCooldown = 0;

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
                // Hover to the side of the player to aim, until a set time (stopAimingTime) remains in attackCooldown
                if(attackCooldown > stopAimingTime){
                    transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y-1, aimPositionOffset.z);
                    warningZone.SetActive(true);
                }
            }

            else if(attackCooldown <= 0){
                // Move towards targeted position, until reaching set thrust distance
                warningZone.SetActive(false);
                if(transform.localPosition.z >= startPosition.z + thrustDistance){
                    rb.velocity = Vector3.zero;
                    StartCoroutine(HitStop());
                }
                else{
                    sfxCooldown -= Time.deltaTime;
                    rb.AddForce(Vector3.back * thrustSpeed * Time.deltaTime);
                    if(sfxCooldown <= 0){
                        audioPlayer.PlayBossThrustClip(transform.position);
                        sfxCooldown = 0.5f;
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(attackCooldown <= 0 && !finishingAttack){
            if(other.GetComponent<Health>()){
                if(other.GetComponent<EnemyHealth>()) // Deal more damage and score to enemies
                     other.GetComponent<Health>().UpdateHealth(-2, 2);
                else
                    other.GetComponent<Health>().UpdateHealth(-1);
            }
        }
    }
}
