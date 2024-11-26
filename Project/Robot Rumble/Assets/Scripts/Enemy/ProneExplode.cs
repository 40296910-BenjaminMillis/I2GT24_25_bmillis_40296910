using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProneExplode : ProneType
{
    [SerializeField] GameObject explosion;

    void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag("Enemy") && isProne){
            // Remove prone enemy and explode, hurting the surroundings
            GetComponent<EnemyHealth>().UpdateHealth(-GetComponent<EnemyHealth>().GetHealth());
            GameObject instance = Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }

}
