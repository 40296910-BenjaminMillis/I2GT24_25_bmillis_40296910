using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] GameObject decoration;
    [SerializeField] float duration = 5f;

    private void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag("Player")){
            Debug.Log("ping");
            StartCoroutine(SetPowerup(collider));
        }
    }

    IEnumerator SetPowerup(Collider player){
        transform.position = Vector3.zero;
        decoration.SetActive(false);
        StartCoroutine(player.GetComponent<PlayerHealth>().SetTempoaryInvincibility(duration));
        yield return new WaitForSeconds(duration+0.1f);
        Destroy(gameObject);
    }
    
}
