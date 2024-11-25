using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    [SerializeField] GameObject decoration;
    [SerializeField] protected float duration = 5f;

    private void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag("Player")){
            StartCoroutine(SetPowerup(collider));
        }
    }

    public virtual IEnumerator SetPowerup(Collider player){
        transform.position = Vector3.zero;
        decoration.SetActive(false);

        yield return new WaitForSeconds(duration+0.1f);
        Destroy(gameObject);
    }
    
}
