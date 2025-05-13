using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] float force = 20f; // Force applied to surrounding rigidbodies
    [SerializeField] float upwardsModifier = 5f; // Additional upward force
    [SerializeField] float maxRadius = 20f; // Size of the explosion
    [SerializeField] float growSpeed = 1.1f; // Explosion starts out small and grows to a set size

    [SerializeField] float slowdownSpeed = 0.1f; // Test!

    MeshRenderer[] meshes;
    float emissionIntensity = 1;
    [SerializeField] float fadeSpeed = 0.3f;

    private void Start() {
        Explode();
        FindObjectOfType<AudioPlayer>().PlayExplosionClip(transform.position);  

        meshes = GetComponentsInChildren<MeshRenderer>(); 
    }

    // Expand to a set size, then destroy
    void FixedUpdate()
    {
        // Make the explosion grow quickly to begin with, and slow down as it reaches its max size
        if(transform.localScale.x >= maxRadius){
            Destroy(gameObject);
        }

        else if(transform.localScale.x >= (maxRadius/2)){
            if(growSpeed > 0.1f)
                growSpeed -= slowdownSpeed;
            else
                growSpeed = 0.1f;
        }

        transform.localScale = transform.localScale + new Vector3(growSpeed, growSpeed, growSpeed); 

        // Past a certain threshold make the explosion fade away
        if(emissionIntensity > -0.5)
            emissionIntensity -= fadeSpeed;
            foreach (MeshRenderer mesh in meshes)
            {
                mesh.material.SetColor("_EmissionColor", mesh.material.color * emissionIntensity);
            }
    }

    // Apply damage and force to all surrounding gameobjects
    void Explode(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxRadius);
        foreach(Collider hit in colliders){
            if(hit.GetComponent<Rigidbody>()){
                hit.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, maxRadius, upwardsModifier, ForceMode.Impulse);
            }
            if(hit.GetComponent<Health>() && damage > 0){ 
                hit.gameObject.GetComponent<Health>().UpdateHealth(-damage);
            }
        }
    }
}
