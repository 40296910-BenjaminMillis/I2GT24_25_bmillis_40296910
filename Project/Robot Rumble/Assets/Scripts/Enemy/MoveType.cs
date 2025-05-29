using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveType : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 1000f; // Speed an enemy will move
    [SerializeField] protected float turnDelay = 0.5f; // The time it takes for the enemy to face the player. Larger values make the enemy take longer to turn
    protected Rigidbody enemyRb;
    protected Transform playerTransform;

    void Start()
    {
        enemyRb = gameObject.GetComponent<Rigidbody>();
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        DifficultySettings difficultySettings = FindObjectOfType<DifficultySettings>();
        moveSpeed *= difficultySettings.GetEnemySpeed()/2;
        turnDelay /= difficultySettings.GetEnemySpeed()/2;
    }

    public virtual void Move(){
        
    }

    virtual protected IEnumerator RotateTowardsPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        float rotationTime = 0f;
        while (rotationTime < turnDelay)
        {
            // Rotate towards the player with a set delay
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationTime / turnDelay);
            yield return null;
            rotationTime += Time.deltaTime;
        }
    }
}
