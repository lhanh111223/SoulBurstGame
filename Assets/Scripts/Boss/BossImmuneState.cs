using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossImmuneState : BossBaseState
{
    private Coroutine attackCoroutine; // Biến để lưu trữ Coroutine từ BossAttackState

    [Header("EnemySpawn")]
    public GameObject MeleeEnemy;
    public GameObject RangeEnemy;
    private bool allEnemiesDestroyed = false;
    private GameObject[] enemyInstances = new GameObject[4];
    private float timer = 1f;
    public override void EnterState(BossStateManager boss)
    {
        Debug.Log("Go to Immune State!");

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
        }
        int count = 0;

        while(count < boss.enemyInstances.Length)
        {
            enemyInstances[count] = Object.Instantiate(boss.enemyInstances[count], boss.spawnPosition[count].position, Quaternion.identity);
            count++;
        }
        
        
    }

    public override void OnCollisionEnter(BossStateManager boss, Collision2D collider)
    {
    }

    public override void UpdateState(BossStateManager boss)
    {
        BossMovement bossMovement = boss.GetComponent<BossMovement>();
        bossMovement.MoveToTarget(boss.startPosition.position);


        timer -= Time.deltaTime;
        if (timer < 0)
        {
            BossAttackRotation bossAttack = boss.GetComponent<BossAttackRotation>();
            bossAttack.LaserAttack();
            timer = 1.5f;
        }
        boss.ChangeAnimationState(BossAnimation.BossImmune.ToString());
        
        allEnemiesDestroyed = true;
        foreach (GameObject enemy in enemyInstances)
        {
            if (enemy != null)
            {
                allEnemiesDestroyed = false;
                break;
            }
        }

        if (allEnemiesDestroyed)
        {
            boss.ChangeAnimationState(BossAnimation.BossIdle.ToString());
            boss.SwitchState(boss.AttackState);
        }
    }

    public void SetAttackCoroutine(Coroutine coroutine)
    {
        attackCoroutine = coroutine;
    }
}
