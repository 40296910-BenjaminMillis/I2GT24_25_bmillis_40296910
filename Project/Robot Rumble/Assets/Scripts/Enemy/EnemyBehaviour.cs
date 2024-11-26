using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] int rank = 1; // Determines how the enemy is weighted in spawning, and affects their actual score rewarded
    [SerializeField] float gravity = 10f;
    [SerializeField] Collider proneTriggerCollider; // Collider used for enemies while prone (being thrown/dashed into)
    [SerializeField] bool immovable; // Whether or not an enemy can be moved or picked up
    [SerializeField] TrailRenderer trailRenderer;

    MoveType moveType;
    AttackType attackType;
    ProneType proneType;
    Transform playerTransform;
    Rigidbody rb;

    bool isActive = true;

    void Awake(){
        moveType = GetComponent<MoveType>();
        attackType = GetComponent<AttackType>();
        proneType = GetComponent<ProneType>();
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        rb = GetComponent<Rigidbody>();
    }   
    
    void Update(){
        if(isActive){
            if(moveType)
                moveType.Move();
            if(attackType && playerTransform)
                attackType.Attack();
            // Enemy gravity is disabled when not active, to give them a proper flight path during a throw/dash
            rb.AddForce(Physics.gravity * gravity * Time.deltaTime, ForceMode.Acceleration); 
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

    // Activated when the enemy is sent flying by a dash or throw. Makes them unable to attack or move
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
