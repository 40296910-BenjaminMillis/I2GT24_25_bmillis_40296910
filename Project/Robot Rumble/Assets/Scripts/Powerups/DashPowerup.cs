using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPowerup : Powerup
{
    public override IEnumerator SetPowerup(Collider player){
        //make the player dash for x amount of seconds

        if(player.GetComponent<PlayerControl>().getDashCooldown() <= 0){
            Debug.Log("setpowerup");
            player.GetComponent<PlayerControl>().SetSuperDash(duration);

            StartCoroutine(base.SetPowerup(player));
        }


        yield return null;
    }
}
