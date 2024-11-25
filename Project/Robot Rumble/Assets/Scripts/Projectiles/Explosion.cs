using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float force = 20f;
    [SerializeField] float upwardsModifier = 5f;
    [SerializeField] float maxRadius = 20f;
    [SerializeField] float growSpeed = 0.1f;

    private void Start() {
        ExplodingShot();
        FindObjectOfType<AudioPlayer>().PlayExplosionClip(transform.position);    
    }

    void FixedUpdate()
    {
        if(transform.localScale.x >= maxRadius){
            Destroy(gameObject);
        }
        // Expand to a set size, then destroy
        transform.localScale = transform.localScale * growSpeed; 
    }

    void ExplodingShot(){
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
