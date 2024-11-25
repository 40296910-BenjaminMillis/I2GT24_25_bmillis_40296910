using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{  
    [SerializeField] int damage = 1;
    [SerializeField] Vector3 locationToRespawn = new Vector3(0, 40, 0); 
    [SerializeField] ParticleSystem smoke;

    AudioPlayer audioPlayer;

    private void Start() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void OnTriggerStay(Collider other) {
        // Deplete all enemy health if they touch the killfloor
        if(other.CompareTag("Enemy")){
            ParticleSystem instance = Instantiate(smoke, other.transform.position + Vector3.up, smoke.transform.rotation);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
            Health targetHealth = other.GetComponent<Health>();
            targetHealth.UpdateHealth(-targetHealth.GetHealth(), 2);
            audioPlayer.PlayEnemyKillfloorClip(other.transform.position);
        }

        // The player will lose some of health if they touch the floor, and respawn at the center of the stage
        else if(other.CompareTag("Player")){
            Health targetHealth = other.GetComponent<Health>();
            targetHealth.UpdateHealth(-damage); 

            RaycastHit respawnLocation;
            Physics.Raycast(locationToRespawn, Vector3.down, out respawnLocation, Mathf.Infinity);

            other.transform.position = respawnLocation.point + Vector3.up;
        }
    }
}