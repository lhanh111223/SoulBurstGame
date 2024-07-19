using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeadState : BossBaseState
{
    private Coroutine attackCoroutine; // Biến để lưu trữ Coroutine từ BossAttackState
    public override void EnterState(BossStateManager boss)
    {
        Debug.Log("Go to Dead State");
        // Dừng Coroutine của BossAttackState nếu đang chạy
        if (attackCoroutine != null)
        {
            boss.StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        // Dừng Invoke của BossMovement nếu đang tính toán đường đi
        BossMovement bossMovement = boss.GetComponent<BossMovement>();
        if (bossMovement != null)
        {
            bossMovement.CancelInvoke("CalculatePath");
            bossMovement.moveSpeed = 0;
        }
        boss.ChangeAnimationState(BossAnimation.BossDead.ToString());
        float timeDead = boss.bossAnim.GetCurrentAnimatorStateInfo(0).length;
        boss.HealthBarBoss.SetActive(false);
        
        boss.Invoke("Destroy", 2.5f);
        GameLogic gameLogic = GameObject.FindAnyObjectByType<GameLogic>();
        gameLogic.Invoke("loadSceneWinGame", 3.5f);
    }

    public override void OnCollisionEnter(BossStateManager boss, Collision2D collider)
    {
        
    }

    public override void UpdateState(BossStateManager boss)
    {
    }
    public void SetAttackCoroutine(Coroutine coroutine)
    {
        attackCoroutine = coroutine;
    }
}
