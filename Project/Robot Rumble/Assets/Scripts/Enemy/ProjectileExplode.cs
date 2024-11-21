using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplode : Projectile
{
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] float explosionForce = 300f;
    [SerializeField] float upwardsModifier = 2.0f;
    [SerializeField] ParticleSystem explosion;
    MoveType moveType;

    void Start(){
        moveType = GetComponent<MoveType>();
    }

    void Update(){
        moveType.Move();
    }

    protected override void RemoveProjectile(){
        Explode();
        base.RemoveProjectile();
    }

    void Explode(){
        ParticleSystem instance = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach(Collider hit in colliders){
            if(hit.GetComponent<ImpactReceiver>()){
                Debug.Log("explosion hit player");
            }

            else if(hit.GetComponent<Rigidbody>()){
                hit.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier);
            }
        }
    }
}
