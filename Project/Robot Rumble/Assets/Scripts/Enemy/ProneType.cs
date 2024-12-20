using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProneType : MonoBehaviour
{
    [SerializeField] protected int damage = 1; // Damage dealt to self and target when colliding
    protected bool isProne;
    protected AudioPlayer audioPlayer;

    void Start(){
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    // When colliding with another enemy, both enemies get hurt
    void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag("Enemy") && isProne){
            collider.gameObject.GetComponent<Health>().UpdateHealth(-damage, 2);
            GetComponent<Health>().UpdateHealth(-damage, 2);
            audioPlayer.PlayDashHitClip(this.transform.position);
        }
    }

    public void SetIsProne(bool value){
        isProne = value;
    }
}
