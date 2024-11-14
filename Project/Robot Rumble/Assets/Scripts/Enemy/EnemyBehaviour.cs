using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaviour : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] int rank = 1; //Determines the scoring of the enemy, from actual score to how they are weighted in spawning
    [SerializeField] float gravity = 10f;

    //will add this to attack type
    [Header("Projectile Stats")]
    [SerializeField] GameObject projectile;
    [SerializeField] float shotDelay = 5;

    [Header("Positions")]
    [SerializeField] Transform firePosition;
    [SerializeField] Collider proneTriggerCollider;

    MoveType moveType;
    AttackType attackType;

    Transform playerTransform;
    float shotCooldown;
    Rigidbody rb;
    Collider enemyCollider;
    TrailRenderer trailRenderer;
    bool isActive = true;

    void Awake(){
        shotCooldown = shotDelay;
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        rb = GetComponent<Rigidbody>();
        enemyCollider = GetComponent<Collider>();
        trailRenderer = GetComponent<TrailRenderer>();
        moveType = GetComponent<MoveType>();
        //attackType = GetComponent<AttackType>();
    }
    
    void Update(){
        if(isActive){
            //Aiming();
            if(moveType)
                moveType.Move();

            // enemyAttackType.attack

            rb.AddForce(Physics.gravity * gravity * Time.deltaTime, ForceMode.Acceleration); //Find a better way to increase gravity
        }
        
    }

    void Aiming(){
        if(playerTransform != null){
            transform.LookAt(playerTransform.position + Vector3.up);
            if(shotCooldown <= 0){
                Shoot();
                shotCooldown = shotDelay;
            }
            else{
                shotCooldown -= Time.deltaTime;
            }
        }
    }

    void Shoot(){
        // Create instance of projectile, which will fly in the direction the enemy was facing
        Instantiate(projectile, firePosition.position, transform.rotation);
    }

    void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag("Enemy")){
            collider.gameObject.GetComponent<Health>().UpdateHealth(-1);
            GetComponent<Health>().UpdateHealth(-1);
        }
    }

    public void SetIsActive(bool isActive){
        this.isActive = isActive;
    }

    public int GetRank(){
        return rank;
    }

    // Activated when the enemy is sent flying by a dash or throw
    public IEnumerator MakeProne(){
        isActive = false;
        proneTriggerCollider.enabled = true;
        trailRenderer.enabled = true;

        // -different enemy types do different effects while prone (explode? piercing? bouncy?)
            // -where do i want to define this?

        yield return new WaitForSeconds(2);
        if(proneTriggerCollider != null){ // Make sure that the enemy still exists when re-enabling
            isActive = true;
            proneTriggerCollider.enabled = false;
            trailRenderer.enabled = false;
        }
    }

}
