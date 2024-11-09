using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrabAndThrow : MonoBehaviour
{
    [Header("Crosshair Images")]
    [SerializeField] RawImage crosshairDefault;
    [SerializeField] RawImage crosshairGrab;
    //[SerializeField] RawImage crosshairThrow;

    [Header("Grab Settings")]
    [SerializeField] GameObject grabIndicator;
    [SerializeField] float grabPickupDistance = 5f;
    [SerializeField] Transform holdPosition;

    [Header("Throw Settings")]
    [SerializeField] float throwSpeed = 20f;

    GameObject heldObject = null;
    UIManager uiManager;

    void Update(){
        Grab();
        if(heldObject != null){
            MoveHeldEnemy();
            Throw();
        }
    }

    void Grab(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, grabPickupDistance) && heldObject == null){
            if (hit.transform.CompareTag("Enemy")) {
                //update cursor to indacte we can pick up the enemy
                crosshairGrab.enabled = true;

                if(Input.GetMouseButton(1)){
                    heldObject = hit.transform.gameObject;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                    heldObject.GetComponent<Collider>().enabled = false;

                    //disable all enemy update behaviour
                    heldObject.GetComponent<EnemyBehaviour>().SetIsActive(false);
                }        
            }
        }
        
        else{
            //reset cursor to normal
            crosshairGrab.enabled = false;
        }
    }

    
    void MoveHeldEnemy(){
        //follow the hold
        heldObject.transform.position = holdPosition.position;
        heldObject.transform.rotation = holdPosition.rotation;
    }


    void Throw(){
        //charge shot?
        //detach the enemy from player
        //re-enable rigidbody and collision
        //move the enemy away from the player
        //set moving enemy to hurt other enemies (isTrigger?)
            //set the throw properties in (a "ThrowBehaviour" script?)
    }
}
