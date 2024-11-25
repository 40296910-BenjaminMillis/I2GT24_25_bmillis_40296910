using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityPowerup : Powerup
{
    public override IEnumerator SetPowerup(Collider player){
        // Make the player invincible to all damage for set duration
        player.GetComponent<PlayerHealth>().SetTemporaryInvincibility(duration);
        StartCoroutine(base.SetPowerup(player));
        yield return new WaitForSeconds(duration+0.1f);
    }
}
