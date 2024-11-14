using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackType : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Attack(){
        // Going to make a good number of these
            // Projectile, exactly like what I've go in EnemyBehaviour
                // Can probably be used for multiple kinds of Projectile, based on speed etc
            // Bounce, to launch the target away
            // Particle? I plan to do a flamethrower type attack, but I need to check how this would actually work
                //I think you can check for particle collisions
    }
}
