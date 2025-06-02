using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{  
    [SerializeField] int damage = 1;
    [SerializeField] Vector3 launchLocation = new Vector3(0, 80, 0);
    [SerializeField] float impactForce = 1500;
    [SerializeField] ParticleSystem smoke;

    AudioPlayer audioPlayer;

    private void Start() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void OnTriggerEnter(Collider other) {
        // Deplete all enemy health if they touch the killfloor
        if(other.CompareTag("Enemy") && other.gameObject.layer != LayerMask.GetMask("Boss")){
            ParticleSystem instance = Instantiate(smoke, other.transform.position + Vector3.up, smoke.transform.rotation);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
            Health targetHealth = other.GetComponent<Health>();
            targetHealth.UpdateHealth(-targetHealth.GetHealth(), 2);
            audioPlayer.PlayEnemyKillfloorClip(other.transform.position);
        }

        // The player will lose some of health if they touch the floor, and be flung towards the center of the stage
        else if(other.CompareTag("Player")){
            Health targetHealth = other.GetComponent<Health>();
            targetHealth.UpdateHealth(-damage); 

            other.GetComponent<ImpactReceiver>().AddImpact(launchLocation - other.transform.position, impactForce);
        }
    }
}