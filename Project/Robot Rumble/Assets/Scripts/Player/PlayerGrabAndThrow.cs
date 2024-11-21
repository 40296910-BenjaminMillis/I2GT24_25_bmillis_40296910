using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrabAndThrow : MonoBehaviour
{
    [Header("Crosshair Images")]
    [SerializeField] RawImage crosshairDefault;
    [SerializeField] RawImage crosshairGrab;
    [SerializeField] RawImage crosshairThrow;

    [Header("Grab Settings")]
    [SerializeField] float grabPickupDistance = 5f;
    [SerializeField] Transform grabPosition;
    [SerializeField] Transform holdPosition;

    [Header("Throw Settings")]
    [SerializeField] float throwBaseSpeed = 20f;
    [SerializeField] float throwChargeSpeed = 4f;
    [SerializeField] float throwChargeBonusMax = 20f;
    [SerializeField] GameObject throwChargeSlider;
    float throwChargeBonus = 0f;

    GameObject heldObject = null;
    UIManager uiManager;

    private void Start() {
        throwChargeSlider.GetComponent<Slider>().maxValue = throwChargeBonusMax;
    }

    void Update(){
        
        if(heldObject == null){
            crosshairThrow.enabled = false;
            throwChargeSlider.SetActive(false);
            Grab();
        }
        else{
            Throw();
        }
    }

    void Grab(){
        RaycastHit hit;
        
        if(Physics.Raycast(grabPosition.position, grabPosition.forward, out hit, grabPickupDistance)){
            if (hit.transform.CompareTag("Enemy") && !hit.transform.gameObject.GetComponent<EnemyBehaviour>().GetImmovable()) {
                Debug.DrawRay(grabPosition.position, grabPosition.forward * hit.distance, Color.yellow);
                //update cursor to indacte we can pick up the enemy
                crosshairGrab.enabled = true;

                if(Input.GetMouseButton(1)){
                    heldObject = hit.transform.gameObject;
                    heldObject.GetComponent<Collider>().enabled = false;

                    //disable all enemy update behaviour
                    heldObject.GetComponent<EnemyBehaviour>().SetIsActive(false);
                    crosshairGrab.enabled = false;
                    crosshairThrow.enabled = true;
                    throwChargeSlider.SetActive(true);
                }        
            }
        }
        
        else{
            //reset cursor to normal
            crosshairGrab.enabled = false;
        }
    }

    void MoveHeldObject(Transform position){
        //follow the hold position
        heldObject.transform.position = position.position;
        heldObject.transform.rotation = position.rotation;
    }

    void Throw(){
        //charge the throw, up to a maxiumum
        if(Input.GetMouseButton(1)){
            //reel the grabbed object backward and increase a power each update
            if(throwChargeBonus < throwChargeBonusMax)
                throwChargeBonus += throwChargeSpeed * Time.deltaTime;
            throwChargeSlider.GetComponent<Slider>().value = throwChargeBonus;
        }

        //release, throw the object based on power multiplier
        if(Input.GetMouseButtonUp(1)){
            Debug.Log("Throw charge: " + throwChargeBonus);
            heldObject.GetComponent<Collider>().enabled = true;
            heldObject.GetComponent<Rigidbody>().AddForce(((holdPosition.forward*2) + (holdPosition.up)) * (throwBaseSpeed + throwChargeBonus), ForceMode.Impulse);
            //heldObject.GetComponent<Rigidbody>().AddTorque(throwChargeBonus, 0, throwChargeBonus, ForceMode.Impulse); 
            //torque effects the angle it is thrown at
            //probably want to only apply torque to the enemy model? maybe in .MakeProne?

            StartCoroutine(heldObject.GetComponent<EnemyBehaviour>().MakeProne());

            heldObject = null;
            throwChargeBonus = 0;
            crosshairThrow.enabled = false;

            throwChargeSlider.GetComponent<Slider>().value = 0;
            throwChargeSlider.SetActive(false);
        }

        else{
            MoveHeldObject(holdPosition);
        }
            
    }
}
