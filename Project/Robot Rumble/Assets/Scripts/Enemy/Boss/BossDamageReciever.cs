using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageReciever : Health
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
