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
            audioPlayer.PlayPlayerDamageClip();
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
        tempoaryInvincibility = true;
        yield return new WaitForSeconds(duration);
        tempoaryInvincibility = false;
    }
}
