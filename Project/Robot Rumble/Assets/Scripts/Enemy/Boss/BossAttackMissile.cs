using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackMissile : BossAttack
{
    [SerializeField] GameObject missile;
    [SerializeField] Light firingLight;
    [SerializeField] float lightBaseIntensity = 1;
    [SerializeField] Animator animator;

    bool fired = false;


    void Awake()
    {
        DifficultySettings difficultySettings = FindObjectOfType<DifficultySettings>();
        attackDelay /= difficultySettings.GetEnemySpeed();
        stopAimingTime /= difficultySettings.GetEnemySpeed();
        timeToNextAttack /= difficultySettings.GetEnemySpeed();
    }

    public override void Attack()
    {
        base.Attack();

        if (!finishingAttack)
        {
            if (attackCooldown > 0)
            {
                animator.SetBool("StartAttack", true);
                firingLight.enabled = true;
                attackCooldown -= Time.deltaTime;
                // Increase intensity of the firing light
                firingLight.intensity += Time.deltaTime * 2;
            }

            // Spawn the missile
            else if (!fired)
            {
                Instantiate(missile, firingLight.transform.position, transform.rotation);
                fired = true;
                firingLight.intensity = lightBaseIntensity;
                firingLight.enabled = false;
                StartCoroutine(HitStop());
            }
        }
    }

    protected override IEnumerator HitStop(){
        yield return new WaitForSeconds(hitDelay);
        finishingAttack = true;
        animator.SetBool("StartAttack", false);
        animator.SetTrigger("EndAttack");
    }

    protected override IEnumerator ReturnToStart(){
        yield return new WaitForSeconds(timeToNextAttack);
        finishingAttack = false;
        isAttacking = false;
        fired = false;
        attackCooldown = attackDelay;
    }
}
