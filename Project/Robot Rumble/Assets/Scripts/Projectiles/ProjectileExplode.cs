using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplode : Projectile
{
    [SerializeField] GameObject explosion;
    MoveType moveType;

    void Start(){
        moveType = GetComponent<MoveType>();
    }

    void Update(){
        projectileLifetime -= Time.deltaTime;
        if(projectileLifetime <= 0){
            RemoveProjectile();
        }
        moveType.Move();
    }

    protected override void RemoveProjectile(){
        //Create an explosion at the impact point
        GameObject instance = Instantiate(explosion, transform.position, Quaternion.identity); 
        base.RemoveProjectile();
    }


}
