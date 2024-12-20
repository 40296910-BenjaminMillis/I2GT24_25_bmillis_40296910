using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaMover : MonoBehaviour
{
    bool rising;
    bool lowering;

    // Manage the movement of an arena, depending if it has been told to rise or lower
    void Update()
    {
        if(transform.position.y >= -0.5){
            rising = false;
            transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
        }

        if(transform.position.y <= -25){
            lowering = false;
            new Vector3(transform.position.x, -25, transform.position.z);
        }

        if(rising)
            transform.Translate(new Vector3(0, 20, 0) * Time.deltaTime);
        else if(lowering)
            transform.Translate(new Vector3(0, -15, 0) * Time.deltaTime);
    }

    public void RiseArena(){
        rising = true;
    }

    public void LowerArena(){
        lowering = true;
    }
}
