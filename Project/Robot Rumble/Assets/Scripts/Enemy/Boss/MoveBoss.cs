using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoss : MoveType
{
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerControl>().transform;
    }

    public override void Move(){
        StartCoroutine(RotateTowardsPlayer());
    }
}
