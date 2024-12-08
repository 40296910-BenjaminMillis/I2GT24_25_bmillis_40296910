using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageReciever : Health
{
    [SerializeField] Health bossHealth;

    public override void UpdateHealth(int value){
        bossHealth.UpdateHealth(value);
    }
}
