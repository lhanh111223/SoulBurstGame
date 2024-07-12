using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEditor.Experimental.GraphView;

public class BossAttackState : BossBaseState
{
    BossMovement bossMovement;
    Transform Player;
    private int rotationBossAttack = 0;
    private bool isCalculatingPath = false;
    public override void EnterState(BossStateManager boss)
    {
        Debug.Log("Go to Attack State!");
        GameObject playerObject = GameObject.FindWithTag("Player");
        Player = playerObject.transform;
        if (playerObject != null)
        {
            bossMovement = boss.GetComponent<BossMovement>();
            bossMovement.InvokeRepeating("CalculatePath", 0f, 0.5f);
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player GameObject is tagged 'Player'.");
        }

    }

    public override void OnCollisionEnter(BossStateManager boss, Collision2D collider)
    {
        //throw new System.NotImplementedException();
    }

    public override void UpdateState(BossStateManager boss)
    {
        Vector2 bossPos = boss.transform.position;
        Vector2 playerPos = Player.transform.position;
        if(Vector2.Distance(bossPos,playerPos) <= bossMovement.PlayerDistance)
        {
            BossAttackRotation bossAttack = boss.GetComponent<BossAttackRotation>();
            if (!bossAttack.isAttack)
            {
                bossAttack.isAttack = true;

                boss.StartCoroutine(PerformAttackSequence(boss, bossAttack));
            }
              
        }
        else
        {
            boss.ChangeBossIdleState();
        }
    }
    private IEnumerator PerformAttackSequence(BossStateManager boss, BossAttackRotation bossAttack)
    {
        float delayAttack = 0;

        if (rotationBossAttack == 0)
        {
            bossMovement.PlayerDistance = 8f;
            boss.ChangeAnimationState(BossAnimation.BossRangeHit.ToString());
            delayAttack = boss.bossAnim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(delayAttack);
            bossAttack.RangeAttack();
        }
        else if (rotationBossAttack == 1)
        {
            bossMovement.PlayerDistance = 4f;
            boss.ChangeAnimationState(BossAnimation.BossLaserCass.ToString());
            delayAttack = boss.bossAnim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(delayAttack);
            bossAttack.LaserAttack();
        }
        else if (rotationBossAttack == 2)
        {
            bossMovement.PlayerDistance = 2f;
            boss.ChangeAnimationState(BossAnimation.BossMeleeHit.ToString());
            delayAttack = boss.bossAnim.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(delayAttack);
            bossAttack.MeleeAttack();
        }

        bossAttack.AttackComplete();

        rotationBossAttack = (rotationBossAttack + 1) % 3;
        boss.ChangeBossIdleState();
    }


}
