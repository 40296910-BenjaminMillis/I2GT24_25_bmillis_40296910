using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    [SerializeField] List<BossAttack> attacks = new List<BossAttack>();
    [SerializeField] MoveBoss head;
    [SerializeField] float entryTime = 5f;
    [SerializeField] Animator animator;
    int selectedAttack = 0;
    float entryCountdown = 0;
    Transform playerTransform;
    AudioPlayer audioPlayer;

    void Start(){
        audioPlayer = FindObjectOfType<AudioPlayer>();
        audioPlayer.PlayBossLaughClip(transform.position + Vector3.back*20 + Vector3.down*15);
        animator.SetTrigger("Spawning");
        playerTransform = FindObjectOfType<PlayerControl>().transform;
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
