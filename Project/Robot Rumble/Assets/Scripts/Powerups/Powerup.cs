using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    [SerializeField] GameObject decoration;
    [SerializeField] protected float duration = 5f;

    AudioPlayer audioPlayer;
    bool hasLanded = false;

    private void Start() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void OnTriggerEnter(Collider collider) {
        if(collider.CompareTag("Player")){
            audioPlayer.PlayPowerupCollectClip(transform.position);
            StartCoroutine(SetPowerup(collider));
        }
    }

    // Make the powerup dissapear and destroy
    public virtual IEnumerator SetPowerup(Collider player){
        transform.position = Vector3.zero;
        decoration.SetActive(false);

        yield return new WaitForSeconds(duration+0.1f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        if(!hasLanded){
            hasLanded = true;
            audioPlayer.PlayPowerupFallClip(transform.position);
        }
    }
    
}
