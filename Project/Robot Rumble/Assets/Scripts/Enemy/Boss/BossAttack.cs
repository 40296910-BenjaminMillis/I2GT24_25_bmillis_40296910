using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack : AttackType
{
    [Header("Boss Attack")]
    [SerializeField] protected GameObject warningZone; // Used to indicate to the player where they should avoid the attack
    [SerializeField] protected float stopAimingTime = 1; // Time set during the attack cooldown where the boss stops tracking the player 
    [SerializeField] protected Vector3 aimPositionOffset; // Position of attack to the player
    [SerializeField] protected float hitDelay = 1f; // Time the attack waits between the end of the attack and returning to idle
    [SerializeField] protected float timeToNextAttack = 3f; // When the attack is done, the time it takes to move to the next boss attack
    protected Vector3 startPosition; // Idle position
    protected bool isAttacking;
    protected bool finishingAttack;

    void Start(){
        attackCooldown = attackDelay;
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        audioPlayer = FindObjectOfType<AudioPlayer>();
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
            return;
        }
    }

    // Wait time after attack has connected
    protected virtual IEnumerator HitStop(){
        yield return new WaitForSeconds(hitDelay);
        finishingAttack = true;
    }

    // Reset attack to normal position and values.
    protected virtual IEnumerator ReturnToStart(){
        yield return new WaitForSeconds(timeToNextAttack);
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
        if(isAttacking)
            audioPlayer.PlayBossChargeClip(transform.position);
    }
}
