using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void Die(){
        base.Die();
        //Remove control from the game, display game over
    }
}
