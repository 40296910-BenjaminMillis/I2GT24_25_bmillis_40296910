using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] float invincibilityFrameLength = 0.5f; // Length invincibility given to the player when taking damage, so that they do not recieve too much damage too quickly
    [SerializeField] PlayerCamera playerCamera;
    [SerializeField] GameObject invincibilityBar; // Representation of invincibility time left, displayed over the healthbar
    AudioPlayer audioPlayer;
    float invincibilityFrameTime = 0;

    void Awake(){
        audioPlayer = FindObjectOfType<AudioPlayer>(); 
        health = maxHealth;
    }

    void Update(){
        if(invincibilityFrameTime > 0){ //Reduces invincibility counter and slider over time
            invincibilityFrameTime -= Time.deltaTime; 
            invincibilityBar.GetComponent<Slider>().value = invincibilityFrameTime;   
        }
        else{
            invincibilityBar.SetActive(false);
        }
    }

    public override void UpdateHealth(int value){
        // If healing
        if(value >= 0){
            base.UpdateHealth(value);
        } 

        else if(invincibilityFrameTime <= 0){ // Only deal damage to the player when not invincible
            invincibilityFrameTime = invincibilityFrameLength; // Give player a short burst of invincibility            
            base.UpdateHealth(value);
            audioPlayer.PlayPlayerDamageClip(this.transform.position);
            StartCoroutine(playerCamera.ScreenShake());
        }
        if(health > maxHealth){ // If exceeding maxHealth, set health to max
            health = maxHealth;
        }
    }

    public override void Die(){
        //Remove player and control from the game
        base.Die();
        //Get the game manager to end the game
        GameStateManager gameStateManager = FindObjectOfType<GameStateManager>();
        gameStateManager.EndGame();
    }

    // Any particle with colliders will trigger this (e.g. flames)
    void OnParticleCollision(){
        UpdateHealth(-1);
    }

    public void SetTemporaryInvincibility(float duration){
        // Set cooldown of invincibility. If powerup was already picked up, reset the cooldown
        Debug.Log("invincibility of " + duration);
        invincibilityFrameTime = duration;
       
        // Enable invincibility bar, indicates length of invincibility time
        invincibilityBar.SetActive(true);
        invincibilityBar.GetComponent<Slider>().maxValue = duration;
        invincibilityBar.GetComponent<Slider>().value = duration;
    }

    public float GetInvincibilityFrameTime(){
        return invincibilityFrameTime;
    }
}
