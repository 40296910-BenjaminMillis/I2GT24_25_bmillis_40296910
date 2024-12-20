using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageReciever : Health // Damage recievers respond to boss health when being hit. Helps to manage the different meshes of the boss head
{
    [SerializeField] Health bossHealth;

    public override void UpdateHealth(int value){
        bossHealth.UpdateHealth(value);
    }

    // Only used if a score multiplier is being added
    public override void UpdateHealth(int value, int scoreMultiplier){ 
        bossHealth.UpdateHealth(value, scoreMultiplier);
    }
}
