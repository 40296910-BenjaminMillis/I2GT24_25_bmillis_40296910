using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProneType : MonoBehaviour
{
    protected bool isProne;
    protected int damage = 1;

    void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag("Enemy") && isProne){
            collider.gameObject.GetComponent<Health>().UpdateHealth(-1, 2);
            GetComponent<Health>().UpdateHealth(-1, 2);
        }
    }

    public void SetIsProne(bool value){
        isProne = value;
    }
}
