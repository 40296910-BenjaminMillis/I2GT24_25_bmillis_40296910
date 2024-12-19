using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    [SerializeField] float baseMoveSpeed = 5; // Speed when no events happening
    [SerializeField] float applauseSpeed = 15; // Speed during end of wave events

    [SerializeField] float hoverHeight;
    [SerializeField] Vector3 hoverPosition;

    float moveSpeed;
    bool rising = true;

    void Start(){
       moveSpeed = baseMoveSpeed;
       transform.position += new Vector3(0, Random.Range(0, hoverHeight), 0);
    }

    void Update(){
        // Move up and down, based on current move speed
        if(transform.position.y <= hoverPosition.y)
            rising = true;
        if(transform.position.y >= hoverPosition.y + hoverHeight)
            rising = false;

        if(rising)
            transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
        else
            transform.position += new Vector3(0, -moveSpeed, 0) * Time.deltaTime;
    }

    public IEnumerator Applause(float time){
        moveSpeed = applauseSpeed;
        yield return new WaitForSeconds(time);
        moveSpeed = baseMoveSpeed; 
    }
}
