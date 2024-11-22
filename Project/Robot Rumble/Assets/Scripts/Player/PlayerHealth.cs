using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] float invincibilityFrameLength = 0.5f;
    [SerializeField] PlayerCamera playerCamera;
    AudioPlayer audioPlayer;
    float invincibilityFrameTime = 0;
    bool tempoaryInvincibility;

    void Awake(){
        audioPlayer = FindObjectOfType<AudioPlayer>();  
    }

    void Update(){
        if(invincibilityFrameTime > 0)
            invincibilityFrameTime -= Time.deltaTime;
    }

    public override void UpdateHealth(int value){
        if(invincibilityFrameTime <= 0 && !tempoaryInvincibility){
            invincibilityFrameTime = invincibilityFrameLength;
            base.UpdateHealth(value);
            audioPlayer.PlayPlayerDamageClip(this.transform.position);
            StartCoroutine(playerCamera.ScreenShake());
        }
    }

    public override void Die(){
        //Remove player and control from the game
        base.Die();
        //Get the game manager to end the game
        GameStateManager gameStateManager = FindObjectOfType<GameStateManager>();
        gameStateManager.EndGame();
    }

    void OnParticleCollision(){
        UpdateHealth(-1);
    }

    public IEnumerator SetTempoaryInvincibility(float duration){
    //check if invincibility is already in place
        Debug.Log("invincibility of " + duration);
        tempoaryInvincibility = true;
        //set healthbar to blue? or do something at least tempoary to indicate invincibility
        //could enable a ui element for "shield" bar over the healthbar, translucent
        yield return new WaitForSeconds(duration);
        Debug.Log("invincibility of " + duration + " seconds is over");
        tempoaryInvincibility = false;
    }
}
