using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodePowerup : Powerup
{
    public override IEnumerator SetPowerup(Collider player){
        // Toggle player explosion shots for set duration
        player.GetComponent<PlayerControl>().SetExplodingShot(duration);
        StartCoroutine(base.SetPowerup(player));
        yield return new WaitForSeconds(duration+0.1f);
    }
}
