using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    MoveType moveType;
    Transform playerTransform;

    bool isActive = true;

    void Start(){
        moveType = GetComponent<MoveType>();
        playerTransform = FindObjectOfType<PlayerControl>().transform;
    }

    void Update(){
        if(playerTransform){
            if(moveType)
                moveType.Move();

            //The boss will have (maybe) 3 attacks:
                //slam the player from above
                    //circle warning zone
                    //drop hammer, explode on impact? easy way to add damage

                //pierce the player through the stage horizontally
                    //cylinder warning zone
                    //thrust lance, trigger collider to hurt as it moves

                //Shoot projectile(s) from mouth
                    //possibly just a volley of rockets?
                    //add a glow to the mouth during attack?

            //after an attack is done, wait for X seconds and choose a new attack, or cycle through a list
            
        }
    }
}
