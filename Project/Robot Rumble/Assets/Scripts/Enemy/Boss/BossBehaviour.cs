using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] List<BossAttack> attacks = new List<BossAttack>();
    [SerializeField] MoveBoss head;
    [SerializeField] float entryTime = 5f;
    [SerializeField] Vector3 hoverPosition;
    int selectedAttack = 0;
    float entryCountdown = 0;
    Transform playerTransform;
    Vector3 startPosition;

    void Start(){
        playerTransform = FindObjectOfType<PlayerControl>().transform;
        startPosition = transform.position;
    }

    void Update(){
        // Do not attack until entry is complete
        if(entryCountdown < entryTime){
            entryCountdown += Time.deltaTime;
            return;
        }

        if(playerTransform){
            head.Move();

            if(!attacks[selectedAttack].GetIsAttacking()){
                // Move to the next attack and set it up
                selectedAttack++;
                if(selectedAttack > attacks.Count-1)
                    selectedAttack = 0;
                attacks[selectedAttack].SetIsAttacking(true);
            }
        }
    }
}
