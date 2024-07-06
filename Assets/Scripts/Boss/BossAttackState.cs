using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossAttackState : BossBaseState
{
    BossMovement bossMovement;
    

    public override void EnterState(BossStateManager boss)
    {
        Debug.Log("Go to Attack State!");
        GameObject playerObject = GameObject.FindWithTag("Player");
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
        throw new System.NotImplementedException();
    }

    public override void UpdateState(BossStateManager boss)
    {
        
    }


}
