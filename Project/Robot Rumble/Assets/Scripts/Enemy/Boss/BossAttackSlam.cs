using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttackSlam : BossAttack
{
    [Header("Slam")]
    [SerializeField] float slamSpeed = 2000; // Fall speed of the attack
    Rigidbody rb;

    void Awake(){
        DifficultySettings difficultySettings = FindObjectOfType<DifficultySettings>();
        attackDelay /= difficultySettings.GetEnemySpeed();
        stopAimingTime /= difficultySettings.GetEnemySpeed();
        hitDelay /= difficultySettings.GetEnemySpeed();
        timeToNextAttack /= difficultySettings.GetEnemySpeed();
        slamSpeed *= difficultySettings.GetEnemySpeed();

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
                // Hover above the player to aim, until a set time (stopAimingTime) remains in attackCooldown
                if(attackCooldown > stopAimingTime){
                    transform.position = playerTransform.position + aimPositionOffset;
                    warningZone.SetActive(true);
                    RaycastHit hitLocation;
                    Physics.Raycast(transform.position, Vector3.down, out hitLocation, Mathf.Infinity);
                    warningZone.transform.position = hitLocation.point;
                }
            }

            else if(attackCooldown <= 0){
                // Fall to the ground and attempt to squish the player
                warningZone.SetActive(false);
                rb.AddForce(Vector3.down * slamSpeed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if(attackCooldown <= 0 && !finishingAttack){
            if(other.GetComponent<Health>()){
                if(other.GetComponent<EnemyHealth>())
                     other.GetComponent<Health>().UpdateHealth(-2, 2);
                else
                    other.GetComponent<Health>().UpdateHealth(-1);
            }
            rb.velocity = Vector3.zero;
            audioPlayer.PlayBossThudClip(transform.position);
            StartCoroutine(HitStop());
        }
    }
}
