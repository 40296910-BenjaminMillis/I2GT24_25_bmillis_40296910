using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveType : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 1000f;
    [SerializeField] protected float turnSpeed = 0.5f;
    protected Rigidbody enemyRb;
    protected Transform playerTransform;

    void Start(){
        enemyRb = gameObject.GetComponent<Rigidbody>();
        playerTransform = FindObjectOfType<PlayerControl>().transform;
    }

    public virtual void Move(){
        
    }

    virtual protected IEnumerator RotateTowardsPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        float rotationTime = 0f;
        while (rotationTime < turnSpeed)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationTime / turnSpeed);
            yield return null;
            rotationTime += Time.deltaTime;
        }
    }
}
