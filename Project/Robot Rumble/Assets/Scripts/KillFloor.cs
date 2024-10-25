using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{  
    [SerializeField] int damage = 1;
    [SerializeField] Vector3 respawnLocation = new Vector3(0, 3, 0); 

    void OnTriggerEnter(Collider other) {
        // Deplete all enemy health if they touch the killfloor
        if(other.tag == "Enemy"){
            Health targetHealth = other.GetComponent<Health>();
            targetHealth.UpdateHealth(-targetHealth.GetHealth()); 
        }

        // The player will lose some of health if they touch the floor, and respawn at the center of the stage
        else if(other.tag == "Player"){
            Health targetHealth = other.GetComponent<Health>();
            targetHealth.UpdateHealth(-damage); 
            other.transform.position = respawnLocation;
        }
    }
}