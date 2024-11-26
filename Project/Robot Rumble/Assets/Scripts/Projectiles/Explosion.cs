using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float force = 20f; // Force applied to surrounding rigidbodies
    [SerializeField] float upwardsModifier = 5f; // Additional upward force
    [SerializeField] float maxRadius = 20f; // Size of the explosion
    [SerializeField] float growSpeed = 0.1f; // Explosion starts out small and grows to a set size

    private void Start() {
        Explode();
        FindObjectOfType<AudioPlayer>().PlayExplosionClip(transform.position);    
    }

    // Expand to a set size, then destroy
    void FixedUpdate()
    {
        if(transform.localScale.x >= maxRadius){
            Destroy(gameObject);
        }
        transform.localScale = transform.localScale * growSpeed; 
    }

    // Apply damage and force to all surrounding gameobjects
    void Explode(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxRadius);
        foreach(Collider hit in colliders){
            if(hit.GetComponent<Rigidbody>()){
                hit.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, maxRadius, upwardsModifier, ForceMode.Impulse);
            }
            if(hit.GetComponent<EnemyHealth>() || hit.GetComponent<PlayerHealth>()){ 
                hit.gameObject.GetComponent<Health>().UpdateHealth(-damage);
            }
        }
    }
}
