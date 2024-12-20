using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLight : MonoBehaviour
{
    Transform target;

    // Rotate to follow the players position
    void Update(){
        if(target != null){
            transform.LookAt(target.position);
        }
        else if(FindObjectOfType<PlayerControl>()){
            target = FindObjectOfType<PlayerControl>().transform;
        }
    }
}
