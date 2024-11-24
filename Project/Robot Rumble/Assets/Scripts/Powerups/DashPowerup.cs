using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPowerup : Powerup
{
    public override IEnumerator SetPowerup(Collider player){
        // Make the player dash for set duration
        if(player.GetComponent<PlayerControl>().getDashCooldown() <= 0){
            player.GetComponent<PlayerControl>().SetSuperDash(duration);

            StartCoroutine(base.SetPowerup(player));
        }
        yield return null;
    }
}
