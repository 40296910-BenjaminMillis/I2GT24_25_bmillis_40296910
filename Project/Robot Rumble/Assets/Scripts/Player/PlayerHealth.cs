using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] float invincibilityFrameLength = 0.5f;
    [SerializeField] PlayerCamera playerCamera;
    [SerializeField] GameObject invincibilityBar;
    AudioPlayer audioPlayer;
    float invincibilityFrameTime = 0;

    void Awake(){
        audioPlayer = FindObjectOfType<AudioPlayer>(); 
    }

    void Update(){
        if(invincibilityFrameTime > 0){
            invincibilityFrameTime -= Time.deltaTime;
            invincibilityBar.GetComponent<Slider>().value = invincibilityFrameTime;   
        }
        else{
            invincibilityBar.SetActive(false);
        }
    }

    public override void UpdateHealth(int value){
        if(invincibilityFrameTime <= 0){
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

    public void SetTemporaryInvincibility(float duration){
        // Check if invincibility is already in place
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
