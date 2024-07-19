using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossAttackState : BossBaseState
{
    HealthBossController healthBoss;
    BossMovement bossMovement;
    Transform Player;
    private bool isBossLowHP = false;
    private int attackIndex = 0; // Cờ để theo dõi loại đòn tấn công cuối cùng
    private int rangeAttackCount = 0; // Cờ để theo dõi số lượng đòn tấn công tầm xa đã thực hiện
    private float slowFactor = 1f; // Hệ số làm chậm
    private bool isAttacking = false; // Cờ để theo dõi xem boss đang thực hiện đòn tấn công hay không
    private Coroutine attackCoroutine; // Biến để lưu trữ Coroutine đang chạy
    private float timer = 3f;

    public override void EnterState(BossStateManager boss)
    {
        Debug.Log("Go to Attack State!");
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            Player = playerObject.transform;
            bossMovement = boss.GetComponent<BossMovement>();
            bossMovement.InvokeRepeating("CalculatePath", 0f, 0.5f);
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player GameObject is tagged 'Player'.");
        }
        if (attackCoroutine != null) { boss.StopCoroutine(attackCoroutine); }
        attackCoroutine = boss.StartCoroutine(PerformAttackSequence(boss));

        BossImmuneState immuneState = boss.ImmuneState as BossImmuneState;
        if (immuneState != null)
        {
            immuneState.SetAttackCoroutine(attackCoroutine);
        }
        healthBoss = boss.HealthBossController;
    }

    public override void OnCollisionEnter(BossStateManager boss, Collision2D collider)
    {
        BossAttackRotation bossAttack = boss.GetComponent<BossAttackRotation>();
        // Implement collision handling logic if needed
        if (collider.gameObject.tag == "Bullet1" && bossAttack.isParry)
        {
            float delayAttack = 0;

            boss.ChangeAnimationState(BossAnimation.BossLaserCass.ToString());
            delayAttack = boss.bossAnim.GetCurrentAnimatorStateInfo(0).length * slowFactor;
            bossAttack.Invoke("LaserAttack", delayAttack);
        }
    }

    public override void UpdateState(BossStateManager boss)
    {
        if (!isAttacking)
        {
            boss.StartCoroutine(PerformAttackSequence(boss));
        }
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            BossAttackRotation bossAttack = boss.GetComponent<BossAttackRotation>();

            bossAttack.LaserAttack();
            timer = 4f;
        }
        if (healthBoss.currentHealth <= (healthBoss.maxHealth * 0.5) && !isBossLowHP)
        {
            boss.SwitchState(boss.ImmuneState);
            isBossLowHP = true;
        }
        if (healthBoss.currentHealth <= 0)
        {
            boss.SwitchState(boss.DeadState);
        }
    }

    private IEnumerator PerformAttackSequence(BossStateManager boss)
    {
        isAttacking = true;
        rangeAttackCount = 0; // Reset số lượng đòn tấn công tầm xa

        // Lặp lại chuỗi đòn tấn công
        while (true)
        {
            Vector2 bossPos = boss.transform.position;
            Vector2 playerPos = Player.transform.position;
            float distanceToPlayer = Vector2.Distance(bossPos, playerPos);
            BossAttackRotation bossAttack = boss.GetComponent<BossAttackRotation>();

            // Đánh melee khi người chơi gần hơn 2m
            if (distanceToPlayer <= 2f)
            {
                bossMovement.PlayerDistance = 2f;
                boss.ChangeAnimationState(BossAnimation.BossMeleeHit.ToString());
                float delayAttack = boss.bossAnim.GetCurrentAnimatorStateInfo(0).length * slowFactor;
                yield return new WaitForSeconds(delayAttack);
                bossAttack.MeleeAttack();
                rangeAttackCount = 0; // Reset đếm tấn công tầm xa sau khi thực hiện đòn melee
            }
            else // Đánh tầm xa trong các trường hợp khác
            {
                bossMovement.PlayerDistance = 4f;
                boss.ChangeAnimationState(BossAnimation.BossRangeHit.ToString());
                float delayAttack = boss.bossAnim.GetCurrentAnimatorStateInfo(0).length * slowFactor;
                yield return new WaitForSeconds(delayAttack);
                bossAttack.RangeAttack();
                rangeAttackCount++; // Tăng số lượng đòn tấn công tầm xa

                // Kiểm tra nếu đã thực hiện 3 đòn tấn công tầm xa, thực hiện parry
                if (rangeAttackCount >= 3)
                {
                    boss.ChangeAnimationState(BossAnimation.BossParry.ToString());
                    float delayParry = boss.bossAnim.GetCurrentAnimatorStateInfo(0).length * slowFactor;
                    bossAttack.Parry();
                    yield return new WaitForSeconds(delayParry);

                    // Reset số lượng đòn tấn công tầm xa sau khi thực hiện parry
                    rangeAttackCount = 0;
                }
            }

            bossAttack.AttackComplete();

            // Đảm bảo boss tiếp tục di chuyển về phía người chơi sau khi thực hiện đòn tấn công
            bossMovement.InvokeRepeating("CalculatePath", 0f, 0.5f);

            // Tạm dừng để boss có thể di chuyển và cập nhật lại vị trí
            yield return new WaitForSeconds(0.1f);
        }

        isAttacking = false;
        boss.ChangeBossIdleState();
    }
}
