using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera frontCamera;
    private float speed = 20f;
    private float turnSpeed = 45f;
    private float forwardInput;
    private float horizontalInput;

    public KeyCode switchKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        //moves the car forward, based on vertical input
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        //turns the car left and right, based on horizontal input
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);


        if(Input.GetKeyDown(switchKey))
        {
            if(mainCamera.enabled == true){
                mainCamera.enabled = false;
                frontCamera.enabled = true;
            }
            else{
                mainCamera.enabled = true;
                frontCamera.enabled = false;
            }

        }


    }
}
