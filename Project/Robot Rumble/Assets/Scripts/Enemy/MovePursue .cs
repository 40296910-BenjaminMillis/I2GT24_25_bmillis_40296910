using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePursue  : MoveType
{
    public override void Move(){
        //move towards the player, at a specified angle and speed
        Vector3 lookDirection = (playerTransform.position - transform.position).normalized;
        StartCoroutine(LookAtOverTime(transform, playerTransform, turnSpeed));

        // transform.LookAt(playerTransform.position + Vector3.up);
        gameObject.GetComponent<Rigidbody>().AddForce(lookDirection * moveSpeed * Time.deltaTime);
    }


    IEnumerator LookAtOverTime(Transform t, Transform target, float dur)
    {
        Quaternion start = t.rotation;
        Quaternion end = Quaternion.LookRotation(target.position - t.position);
        float rotationTime = 0f;
        while (rotationTime < dur)
        {
            t.rotation = Quaternion.Slerp(start, end, rotationTime / dur);
            yield return null;
            rotationTime += Time.deltaTime;
        }
    }
}
