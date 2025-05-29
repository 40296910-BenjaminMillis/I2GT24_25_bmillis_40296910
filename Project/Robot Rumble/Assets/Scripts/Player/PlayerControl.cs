using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Camera playerCamera;
    [SerializeField] float walkSpeed = 20f;
    
    [Header("Jumping")]
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] float gravity = 10f;

    [Header("Camera")]
    [SerializeField] float lookSensitivity = 5f; // Speed that the camera will rotate with mouse movement
    [SerializeField] float lookXLimit = 90f; // Restricts the angle you can move the camera up and down

    [Header("Shooting")]
    [SerializeField] float fireCooldown = 0.5f; // Time delay between each shot
    [SerializeField] float fireLineDuration = 0.1f; // Time that the line renderer shows the shot
    [SerializeField] float fireLineShrinkAmount = 0.1f; // Make the fire line shrink after being shown
    [SerializeField] Transform firePosition; // The position that the shot comes from
    [SerializeField] ParticleSystem hitSpark;
    [SerializeField] GameObject explosionSphere;
    [SerializeField] GameObject explosiveShotBar; // Shows powerup length
    [SerializeField] GameObject gunModel;
    [SerializeField] float animationSpeed = 0.1f;
    float fireDuration = 0;
    float fireLineWidth = 0.15f;

    [Header("Dashing")]
    [SerializeField] float dashSpeed = 25f;
    [SerializeField] float dashForce = 5f; // How far enemies will be launched by a dash collision
    [SerializeField] float dashDuration = 0.5f; // How long the dash lasts
    [SerializeField] float dashDelay = 2f; // Determines the length of the dash cooldown, time before the next dash
    float dashCooldown = 0;

    CharacterController controller;
    Vector3 moveDirection;
    float xRotation = 0;
    LineRenderer fireLine;
    AudioPlayer audioPlayer;
    Collider dashCollider;
    float explodingShotsTime = 0;

    void Start(){
        audioPlayer = FindObjectOfType<AudioPlayer>();
        controller = GetComponent<CharacterController>();
        fireLine = GameObject.Find("FireLine").GetComponent<LineRenderer>();
        fireLine.enabled = false;
        dashCollider = GetComponent<BoxCollider>();
        SetLookSensitivity();
    }

    void FixedUpdate(){
        Move();
        RotateCamera();
        Shoot();
        if (Input.GetKey(KeyCode.LeftShift) && !dashCollider.enabled && dashCooldown <= 0){
            dashCooldown = dashDelay;
            StartCoroutine(Dash(dashDuration));
        }
        dashCooldown -= Time.deltaTime;

        if(fireDuration > 0){  
            fireDuration -= Time.deltaTime;
            fireLine.startWidth -= fireLineShrinkAmount * Time.deltaTime;
            fireLine.endWidth -= fireLineShrinkAmount * Time.deltaTime;
        }

        if(explodingShotsTime > 0){
            explodingShotsTime -= Time.deltaTime;
            explosiveShotBar.GetComponent<Slider>().value = explodingShotsTime;
        }
        else{
            explosiveShotBar.SetActive(false);
        }
    }

    void Move(){
        // Running
        Vector3 playerInput = new Vector3(Input.GetAxis("Horizontal") * walkSpeed, 0f, Input.GetAxis("Vertical") * walkSpeed);

        // If trying to dash, force the player to move forward
        if(dashCollider.enabled){
            if(playerCamera.fieldOfView > 80)
                playerCamera.fieldOfView -= 4; // Reduce the field of view to make the player feel like they are moving fast
            playerInput = new Vector3(Input.GetAxis("Horizontal") * walkSpeed / 1.5f, 0, 1 * dashSpeed);
        }
        else if(playerCamera.fieldOfView < 90){
            playerCamera.fieldOfView += 2; // Reset changes made to field of view
        }

        float movementDirectionY = moveDirection.y;
        moveDirection = transform.TransformDirection(playerInput);

        // Jumping
        if (Input.GetButton("Jump") && controller.isGrounded){
            moveDirection.y = jumpHeight;
        }
        else if(controller.isGrounded){ // If grounded, reset Y force
            moveDirection.y = 0;
        }
        else{
            moveDirection.y = movementDirectionY;
        }
        if (!controller.isGrounded){
            moveDirection.y -= gravity * Time.deltaTime; // Fall
        }
        // Add all movement
        controller.Move(moveDirection * Time.deltaTime);
    }

    // Rotate the players camera based on sensitivity
    void RotateCamera(){
        xRotation += -Input.GetAxis("Mouse Y") * lookSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSensitivity * Time.deltaTime, 0);
    }

    // Move the player forward temporarily at a faster speed
    IEnumerator Dash(float duration){
        audioPlayer.PlayDashWooshClip(this.transform.position);
        dashCollider.enabled = true; // A trigger collider that is set to push away enemies
        if(GetComponent<PlayerHealth>().GetInvincibilityFrameTime() <= 0)
            GetComponent<PlayerHealth>().SetTemporaryInvincibility(duration);
        yield return new WaitForSeconds(duration);
        dashCollider.enabled = false;
    }

    // If the player collides with an enemy while dashing, bounce them forward and slightly upward
    void OnTriggerEnter(Collider other){
        if(dashCollider.enabled && other.CompareTag("Enemy")){
            if(!other.GetComponent<EnemyBehaviour>().GetImmovable()){
                Vector3 awayFromPlayer = (other.transform.position - transform.position)*2;
                other.GetComponent<Rigidbody>().AddForce((awayFromPlayer + Vector3.up) * dashForce, ForceMode.Impulse);
                StartCoroutine(other.GetComponent<EnemyBehaviour>().MakeProne()); // Make enemy unable to move or attack
                PlayHitEffect(other.transform.position);
                audioPlayer.PlayDashHitClip(this.transform.position);
            }
        }
    }

    // I might want to have this in its own script, if I want more than 1 gun
    // Let the player shoot a ray and hurt enemies
    void Shoot(){
        if(Input.GetButton("Fire1") && fireDuration <= 0){

            //make the gun turn up, then back down??
            StartCoroutine(PlayGunRecoilAnim());

            fireDuration = fireCooldown;
            RaycastHit hit;
            Vector3 hitLocation;

            // Check if the raycast collides with anything
            if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)){
                Debug.Log("Shot Hit");
                if(hit.collider.CompareTag("Enemy")){ // If the raycast hit an enemy, reduce the enemys health
                    Debug.Log("Shot Hit Enemy");
                    var hitReciver = hit.collider.gameObject.GetComponent<Health>();
                    hitReciver.UpdateHealth(-1);
                    PlayHitEffect(hit.point);
                }
                hitLocation = hit.point;

                if(explodingShotsTime > 0){ // If the explosive shot powerup has been picked up, add an explosion at the hit point
                    GameObject instance = Instantiate(explosionSphere, hitLocation, Quaternion.identity);
                }
            }
            else{ // If the raycast did not hit anything at all, send the hit location to a set distance
                Debug.Log("Did not Hit");
                hitLocation = firePosition.TransformDirection(Vector3.forward) * 1000;
            }
            audioPlayer.PlayShootingClip(this.transform.position);
            StartCoroutine(PlayFireLineEffect(hitLocation)); // Display the shot based on where the raycast hit
        }
    }

    // A line to represent a laser blast, from the fire position to the set hit location
    IEnumerator PlayFireLineEffect(Vector3 hitLocation){
        fireLine.startWidth = fireLineWidth;
        fireLine.endWidth = fireLineWidth;
        fireLine.enabled = true;
        fireLine.SetPosition(0, firePosition.position);
        fireLine.SetPosition(1, hitLocation);
        yield return new WaitForSeconds(fireLineDuration);
        fireLine.enabled = false;
        fireLine.startWidth = fireLineWidth;
        fireLine.endWidth = fireLineWidth;
    }

    // Play impact point effect when hitting an enemy
    void PlayHitEffect(Vector3 hitLocation){
        ParticleSystem instance = Instantiate(hitSpark, hitLocation, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }

    // Make the gun model move up and down, to look like recoil
    IEnumerator PlayGunRecoilAnim()
    {
        gunModel.transform.Rotate(new Vector3(12, 0, 0));
        gunModel.transform.localPosition = new Vector3(0.45f, -0.25f, 0.79f);

        yield return new WaitForSeconds(animationSpeed);
        gunModel.transform.Rotate(new Vector3(-12, 0, 0));
        gunModel.transform.localPosition = new Vector3(0.450000763f, -0.350000024f, 0.82f);
    }

    public float getDashCooldown(){
        return dashCooldown;
    }

    public float getDashDelay(){
        return dashDelay;
    }

    public void SetLookSensitivity(){
        lookSensitivity = PlayerPrefs.GetFloat("sensitivity");
    }

    public void SetExplodingShot(float duration){
        // Set cooldown of exploding shots. If powerup was already picked up, reset the cooldown
        explodingShotsTime = duration;
       
        // Enable exploding shots bar, indicates length of powerup
        explosiveShotBar.SetActive(true);
        explosiveShotBar.GetComponent<Slider>().maxValue = duration;
        explosiveShotBar.GetComponent<Slider>().value = duration;
    }
}
