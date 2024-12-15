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
    [SerializeField] float grabPickupDistance = 5f; // Max distance the player needs to be to be able to pick up an enemy 
    [SerializeField] Transform grabPosition;
    [SerializeField] Transform holdPosition;

    [Header("Throw Settings")]
    [SerializeField] float throwBaseSpeed = 20f;  // Speed of a throw without charging
    [SerializeField] float throwChargeSpeed = 4f; // Speed that the throw charges
    [SerializeField] float throwChargeBonusMax = 20f; // Max charge bonus to throw speed
    [SerializeField] GameObject throwChargeSlider;
    float throwChargeBonus = 0f;

    GameObject heldObject = null; // The enemy/object currently being held
    UIManager uiManager;
    CharacterController characterController;

    private void Start() {
        throwChargeSlider.GetComponent<Slider>().maxValue = throwChargeBonusMax;
        characterController = GetComponent<CharacterController>();
    }

    void Update(){
        if(heldObject == null){ // If not holding anything, grab
            crosshairThrow.enabled = false;
            throwChargeSlider.SetActive(false);
            Grab();
        }
        else{ // If holding something, throw
            Throw();
        }
    }

    void Grab(){
        RaycastHit hit;
        if(Physics.Raycast(grabPosition.position, grabPosition.forward, out hit, grabPickupDistance)){ // Raycast a set distance
            if (hit.transform.gameObject.GetComponent<EnemyBehaviour>() && !hit.transform.gameObject.GetComponent<EnemyBehaviour>().GetImmovable()) {
                // Update cursor to indacte we can pick up the enemy
                crosshairGrab.enabled = true;
                Debug.DrawRay(grabPosition.position, grabPosition.forward * hit.distance, Color.yellow);

                if(Input.GetMouseButton(1)){ // If right click while raycast hits enemy, pick the enem
                    heldObject = hit.transform.gameObject;
                    heldObject.GetComponent<Collider>().enabled = false;

                    // Disable all enemy update behaviour
                    heldObject.GetComponent<EnemyBehaviour>().SetIsActive(false);
                    crosshairGrab.enabled = false;
                    crosshairThrow.enabled = true;
                    throwChargeSlider.SetActive(true);
                }        
            }
        }
        
        else{
            // Reset cursor to normal
            crosshairGrab.enabled = false;
        }
    }

    // Have the held enemy follow the hold position
    void MoveHeldObject(Transform position){
        heldObject.transform.position = position.position;
        heldObject.transform.rotation = position.rotation;
    }

    void Throw(){
        // Charge the throw, up to the set maxiumum
        if(Input.GetMouseButton(1)){
            // Increase slider and power each update
            if(throwChargeBonus < throwChargeBonusMax)
                throwChargeBonus += throwChargeSpeed * Time.deltaTime;
            throwChargeSlider.GetComponent<Slider>().value = throwChargeBonus;
        }

        // Release, throw the object based on power multiplier
        if(Input.GetMouseButtonUp(1)){
            Debug.Log("Throw charge: " + throwChargeBonus);
            heldObject.GetComponent<Collider>().enabled = true;
            heldObject.transform.position = transform.position;
            StartCoroutine(IgnoreEnemyColliders());
            
            heldObject.GetComponent<Rigidbody>().AddForce(((holdPosition.forward*2) + (holdPosition.up)) * (throwBaseSpeed + throwChargeBonus), ForceMode.Impulse);
            StartCoroutine(heldObject.GetComponent<EnemyBehaviour>().MakeProne()); // Make the enemy unable to attack or move

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

    // Ignore enemy colliders when throwing an enemy
    IEnumerator IgnoreEnemyColliders(){
        characterController.excludeLayers = LayerMask.GetMask("Enemy");
        yield return new WaitForSeconds(0.1f);
        characterController.excludeLayers = LayerMask.GetMask("Nothing");
    }
}
