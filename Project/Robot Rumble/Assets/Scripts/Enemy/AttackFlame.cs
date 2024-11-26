using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackFlame : AttackType
{
    [SerializeField] ParticleSystem flames;
    
    private void Update(){

    }
    
    public override void Attack(){
        // Currently does not require any scripting, as the flame particle effect automatically deals damage to the player
    }
}
