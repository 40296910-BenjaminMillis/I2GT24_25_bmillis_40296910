using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoss : MoveType
{
    [SerializeField] float hoverSpeed = 50f;
    [SerializeField] float hoverHeight = 5f;
    Vector3 startPosition;
    bool rising = true;

    void Start()
    {
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        startPosition = transform.position;
    }

    private void Update(){
        // Make the boss hover in place
        if(transform.position.y <= startPosition.y)
            rising = true;
        if(transform.position.y >= startPosition.y + hoverHeight)
            rising = false;

        if(rising)
            transform.position += new Vector3(0, hoverSpeed, 0) * Time.deltaTime;
        else
            transform.position += new Vector3(0, -hoverSpeed, 0) * Time.deltaTime;
    }

    public override void Move(){
        StartCoroutine(RotateTowardsPlayer());
    }
}
