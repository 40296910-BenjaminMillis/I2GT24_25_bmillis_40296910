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
        moveType.Move();
    }

    protected override void RemoveProjectile(){
        GameObject instance = Instantiate(explosion, transform.position, Quaternion.identity);
        base.RemoveProjectile();
    }


}