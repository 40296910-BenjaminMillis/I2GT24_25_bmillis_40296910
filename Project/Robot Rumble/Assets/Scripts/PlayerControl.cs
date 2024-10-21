using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControl : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] Camera playerCamera;
    [SerializeField] float moveSpeed = 5f;
    [Header("Jumping")]
    [SerializeField] float jumpForce = 10f;
    //[SerializeField] float gravity = 10f;
    [Header("Camera")]
    [SerializeField] float lookSpeed = 5f;
    [SerializeField] float lookXLimit = 90f; //restrict the angle you can move the camera up and down

    Rigidbody rb;
    Vector3 moveDirection = Vector3.zero;
    bool isGrounded;
    float xRotation = 0;
    
    void Start(){
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update(){
        Move();
        //Jump();
        RotateCamera();
    }

    void Move(){
        float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zMove = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        transform.Translate(xMove, 0, zMove);
    }

    void Jump(){
        // if (Input.GetButton("Jump") && isGrounded){
        //     isGrounded = false;
        //     rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        // }
    }

    void RotateCamera(){
        xRotation += -Input.GetAxis("Mouse Y") * lookSpeed;
        xRotation = Mathf.Clamp(xRotation, -lookXLimit, lookXLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Ground")){ 
            isGrounded = true; 
        }
    }

}
