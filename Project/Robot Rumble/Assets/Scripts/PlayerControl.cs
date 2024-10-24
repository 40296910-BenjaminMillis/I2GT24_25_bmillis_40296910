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

    [Header("Camera")]
    [SerializeField] float lookSpeed = 5f;
    [SerializeField] float lookXLimit = 90f; //restrict the angle you can move the camera up and down

    [Header("Shooting")]
    [SerializeField] LayerMask layersToHit;
    [SerializeField] float fireCooldown = 0.5f;
    [SerializeField] Transform firePosition;
    [SerializeField] LineRenderer fireLine;
    [SerializeField] ParticleSystem fireBlast;

    Rigidbody rb;
    Vector3 moveDirection;

    bool isGrounded;
    float xRotation = 0;

    void Start(){
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate(){
        Move();
        Jump();
        RotateCamera();
        Shoot();
    }

    void Move(){
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        moveDirection = (transform.forward * zMove) + (transform.right * xMove);
        rb.AddForce(moveDirection.normalized * moveSpeed * Time.deltaTime, ForceMode.Force);
    }

    void Jump(){
        if (Input.GetButton("Jump") && isGrounded){
            isGrounded = false; 
            rb.velocity += new Vector3(0f, jumpHeight, 0f);        
        }
    }

    void RotateCamera(){
        xRotation += -Input.GetAxis("Mouse Y") * lookSpeed;
        xRotation = Mathf.Clamp(xRotation, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        firePosition.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Ground")){ 
            isGrounded = true;
        }
    }

    void Shoot(){
        if(Input.GetButton("Fire1") && !fireLine.enabled){
            RaycastHit hit;
            Vector3 hitLocation = Vector3.zero;

            //Check if the ray collides with anything
            if(Physics.Raycast(firePosition.position, firePosition.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)){
                Debug.Log("Shot Hit");
                if(hit.collider.CompareTag("Enemy")){
                    Debug.Log("Shot Hit Enemy");
                    var hitReciver = hit.collider.gameObject.GetComponent<Health>();
                    hitReciver.UpdateHealth(-1); //should damage be stored as a SerializeField?
                    PlayHitEffect(hit.point); //Only want a hit effect if it hits the enemy
                }
                hitLocation = hit.point;
            }
            else{
                Debug.Log("Did not Hit");
                hitLocation = firePosition.TransformDirection(Vector3.forward) * 1000;
            }
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
        ParticleSystem instance = Instantiate(fireBlast, hitLocation, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);

    }
}
