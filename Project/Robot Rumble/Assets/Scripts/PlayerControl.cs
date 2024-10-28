using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Camera playerCamera;
    [SerializeField] float moveSpeed = 1000f;
    
    [Header("Jumping")]
    [SerializeField] float jumpHeight = 10f;
    [SerializeField] float gravity = 10f;

    [Header("Camera")]
    [SerializeField] float lookSpeed = 5f;
    [SerializeField] float lookXLimit = 90f; //restrict the angle you can move the camera up and down

    [Header("Shooting")]
    [SerializeField] LayerMask layersToHit;
    [SerializeField] float fireCooldown = 0.5f;
    [SerializeField] Transform firePosition;
    [SerializeField] ParticleSystem hitSpark;

    CharacterController controller;
    Vector3 moveDirection;
    float xRotation = 0;
    LineRenderer fireLine;
    AudioPlayer audioPlayer;

    void Start(){
        audioPlayer = FindObjectOfType<AudioPlayer>();
        controller = GetComponent<CharacterController>();
        fireLine = GameObject.Find("FireLine").GetComponent<LineRenderer>();
        fireLine.enabled = false;
    }

    void FixedUpdate(){
        Move();
        RotateCamera();
        Shoot();
    }

    void Move(){
        // Running
        Vector3 playerInput = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0f, Input.GetAxis("Vertical") * moveSpeed);
        float movementDirectionY = moveDirection.y;
        moveDirection = transform.TransformDirection(playerInput);

        // Jumping
        if (Input.GetButton("Jump") && controller.isGrounded){
            moveDirection.y = jumpHeight;
        }
        else{
            moveDirection.y = movementDirectionY;
        }
        if (!controller.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Add all movement
        controller.Move(moveDirection * Time.deltaTime);
    }

    void RotateCamera(){
        xRotation += -Input.GetAxis("Mouse Y") * lookSpeed;
        xRotation = Mathf.Clamp(xRotation, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        firePosition.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
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
}
