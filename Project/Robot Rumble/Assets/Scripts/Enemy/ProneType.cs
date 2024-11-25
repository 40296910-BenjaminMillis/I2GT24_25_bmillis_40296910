using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProneType : MonoBehaviour
{
    [SerializeField] protected int damage = 1;
    protected bool isProne;

    void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag("Enemy") && isProne){
            collider.gameObject.GetComponent<Health>().UpdateHealth(-damage, 2);
            GetComponent<Health>().UpdateHealth(-damage, 2);
        }
    }

    public void SetIsProne(bool value){
        isProne = value;
    }
}
