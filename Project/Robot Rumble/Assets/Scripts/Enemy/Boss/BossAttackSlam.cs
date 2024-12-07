using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttackSlam : AttackType
{
    [Header("Slam")]
    [SerializeField] float slamSpeed = 2000;
    Rigidbody rb;
    Vector3 startPosition;
    bool isAttacking;
    bool finishingAttack;

    void Start(){
        attackCooldown = attackDelay;
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        audioPlayer = FindObjectOfType<AudioPlayer>();
        rb = GetComponent<Rigidbody>();
        startPosition = transform.localPosition;
    }

    private void Update(){
        if(isAttacking){
            Attack();
        }
        else{
            //idle stuff
        }
    }

    public override void Attack(){
        base.Attack();

        if(finishingAttack){
            // Return to original position
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, attackDelay * Time.deltaTime);
            StartCoroutine(ReturnToStart());
        }

        else if(attackCooldown > 0){
            attackCooldown -= Time.deltaTime;
            // Hover above the player to aim, until 1 second remains
            if(attackCooldown > 1){
                transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y+40, playerTransform.position.z);
            }
        }

        else if(attackCooldown <= 0){
            // Fall to the ground and attempt to squish the player
            rb.AddForce(Vector3.down * slamSpeed * Time.deltaTime);
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

    IEnumerator HitStop(){
        yield return new WaitForSeconds(2);
        finishingAttack = true;
    }

    IEnumerator ReturnToStart(){
        yield return new WaitForSeconds(attackDelay);
        transform.localPosition = startPosition;
        finishingAttack = false;
        isAttacking = false;
        attackCooldown = attackDelay;
    }

    public bool GetIsAttacking(){
        return isAttacking;
    }

    public void SetIsAttacking(bool value){
        isAttacking = value;
    }

}
