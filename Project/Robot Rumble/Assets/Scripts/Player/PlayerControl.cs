using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Camera playerCamera;
    [SerializeField] float walkSpeed = 20f;
    
    [Header("Jumping")]
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] float gravity = 10f;

    [Header("Camera")]
    [SerializeField] float lookSpeed = 5f; //5f for a desktop build, 1.5f for webgl build
    [SerializeField] float lookXLimit = 90f; //restrict the angle you can move the camera up and down

    [Header("Shooting")]
    [SerializeField] LayerMask layersToHit;
    [SerializeField] float fireCooldown = 0.5f;
    [SerializeField] Transform firePosition;
    [SerializeField] ParticleSystem hitSpark;


    [Header("Dashing")]
    [SerializeField] float dashSpeed = 25f;
    [SerializeField] float dashForce = 5f; // How far enemies will be launched by a dash collision
    [SerializeField] float dashDuration = 0.5f; // How long the dash lasts
    [SerializeField] float dashDelay = 2f;
    float dashCooldown = 0;

    CharacterController controller;
    Vector3 moveDirection;
    float xRotation = 0;
    LineRenderer fireLine;
    AudioPlayer audioPlayer;
    Collider dashCollider;

    void Start(){
        audioPlayer = FindObjectOfType<AudioPlayer>();
        controller = GetComponent<CharacterController>();
        fireLine = GameObject.Find("FireLine").GetComponent<LineRenderer>();
        fireLine.enabled = false;
        dashCollider = GetComponent<BoxCollider>();
    }

    void FixedUpdate(){
        Move();
        RotateCamera();
        Shoot();
        if (Input.GetKey(KeyCode.LeftShift) && !dashCollider.enabled && dashCooldown <= 0){
            dashCooldown = dashDelay;
            //StartCoroutine(Dash());
        }
        dashCooldown -= Time.deltaTime;
    }

    void Move(){
        // Running
        Vector3 playerInput = new Vector3(Input.GetAxis("Horizontal") * walkSpeed, 0f, Input.GetAxis("Vertical") * walkSpeed);
        // If trying to dash, move forward
        if(dashCollider.enabled){
            if(playerCamera.fieldOfView > 80)
                playerCamera.fieldOfView -= 4;
            playerInput = new Vector3(Input.GetAxis("Horizontal") * walkSpeed / 1.5f, 0, 1 * dashSpeed);
        }
        else if(playerCamera.fieldOfView < 90){
            playerCamera.fieldOfView += 2; 
        }

        float movementDirectionY = moveDirection.y;
        moveDirection = transform.TransformDirection(playerInput);

        // Jumping
        if (Input.GetButton("Jump") && controller.isGrounded){
            moveDirection.y = jumpHeight;
        }
        else{
            moveDirection.y = movementDirectionY;
        }
        if (!controller.isGrounded){
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Add all movement
        controller.Move(moveDirection * Time.deltaTime);
    }

    void RotateCamera(){
        xRotation += -Input.GetAxis("Mouse Y") * lookSpeed;
        xRotation = Mathf.Clamp(xRotation, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    // Move the player forward temporarily at a faster speed
    IEnumerator Dash(){
        audioPlayer.PlayDashWooshClip();
        //playerCamera.fieldOfView = 80;
        dashCollider.enabled = true;
        yield return new WaitForSeconds(dashDuration);
        
        //playerCamera.fieldOfView = 90;
        dashCollider.enabled = false;
    }

    // If the player collides with an enemy while dashing, bounce them forward and slightly upward
    void OnTriggerEnter(Collider other){
        if(dashCollider.enabled && other.CompareTag("Enemy")){
            Vector3 awayFromPlayer = (other.transform.position - transform.position)*2;
            other.GetComponent<Rigidbody>().AddForce((awayFromPlayer + Vector3.up) * dashForce, ForceMode.Impulse);
            StartCoroutine(other.GetComponent<EnemyBehaviour>().MakeProne());
            PlayHitEffect(other.transform.position);
            audioPlayer.PlayDashHitClip();
        }
    }

    // I might want to have this in its own script, if I want more than 1 gun
    void Shoot(){
        if(Input.GetButton("Fire1") && !fireLine.enabled){
            RaycastHit hit;
            Vector3 hitLocation;

            //Check if the ray collides with anything
            if(Physics.Raycast(firePosition.position, firePosition.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)){
                Debug.Log("Shot Hit");
                if(hit.collider.CompareTag("Enemy")){
                    Debug.Log("Shot Hit Enemy");
                    var hitReciver = hit.collider.gameObject.GetComponent<Health>();
                    hitReciver.UpdateHealth(-1);
                    PlayHitEffect(hit.point);
                }
                hitLocation = hit.point;
            }
            else{
                Debug.Log("Did not Hit");
                hitLocation = firePosition.TransformDirection(Vector3.forward) * 1000;
            }
            audioPlayer.PlayShootingClip();
            StartCoroutine(PlayFireLineEffect(hitLocation));
        }
    }

    // A line to represent a laser blast
    IEnumerator PlayFireLineEffect(Vector3 hitLocation){
        fireLine.enabled = true;
        fireLine.SetPosition(0, firePosition.position);
        fireLine.SetPosition(1, hitLocation);
        yield return new WaitForSeconds(fireCooldown);
        fireLine.enabled = false;
    }

    // Play impact point effect
    void PlayHitEffect(Vector3 hitLocation){
        ParticleSystem instance = Instantiate(hitSpark, hitLocation, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }

    public float getDashCooldown(){
        return dashCooldown;
    }

    public float getDashDelay(){
        return dashDelay;
    }
}