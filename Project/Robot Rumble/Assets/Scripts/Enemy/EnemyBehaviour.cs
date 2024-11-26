using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] int rank = 1; //Determines the scoring of the enemy, from actual score to how they are weighted in spawning
    [SerializeField] float gravity = 10f;
    [SerializeField] Collider proneTriggerCollider;
    [SerializeField] bool immovable;
    [SerializeField] TrailRenderer trailRenderer;

    MoveType moveType;
    AttackType attackType;
    Transform playerTransform;
    Rigidbody rb;
    ProneType proneType;

    bool isActive = true;

    void Awake(){
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        rb = GetComponent<Rigidbody>();
        moveType = GetComponent<MoveType>();
        attackType = GetComponent<AttackType>();
        proneType = GetComponent<ProneType>();
    }
    
    void Update(){
        if(isActive){
            if(moveType)
                moveType.Move();
            if(attackType && playerTransform)
                attackType.Attack();

            rb.AddForce(Physics.gravity * gravity * Time.deltaTime, ForceMode.Acceleration); //Find a better way to increase gravity
        }
    }

    public void SetIsActive(bool isActive){
        this.isActive = isActive;
    }

    public int GetRank(){
        return rank;
    }

    public bool GetImmovable(){
        return immovable;
    }

    // Activated when the enemy is sent flying by a dash or throw
    public IEnumerator MakeProne(){
        isActive = false;
        proneTriggerCollider.enabled = true;
        trailRenderer.enabled = true;
        
        proneType.SetIsProne(true);

        yield return new WaitForSeconds(2);
        if(proneTriggerCollider != null){ // Make sure that the enemy still exists when re-enabling
            isActive = true;
            proneTriggerCollider.enabled = false;
            trailRenderer.enabled = false;
            proneType.SetIsProne(false);
        }
    }
}
