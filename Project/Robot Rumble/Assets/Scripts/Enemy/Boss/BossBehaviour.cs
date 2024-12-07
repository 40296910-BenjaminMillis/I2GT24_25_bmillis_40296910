using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] List<BossAttackSlam> attacks = new List<BossAttackSlam>();
    [SerializeField] MoveType head;
    int selectedAttack = 0;
    Transform playerTransform;

    void Start(){
        playerTransform = FindObjectOfType<PlayerControl>().transform;
    }

    void Update(){
        if(playerTransform){
            head.Move();

        if(!attacks[selectedAttack].GetIsAttacking()){
            // Move to the next attack and set it up
            selectedAttack++;
            if(selectedAttack > attacks.Count-1)
                selectedAttack = 0;
            attacks[selectedAttack].SetIsAttacking(true);
        }

            //track position in list?
            //if selected attacks cooldown = 0
                //select the next attack in the list

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
