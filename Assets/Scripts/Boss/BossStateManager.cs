using Assets.Scripts.Boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossAnimation
{
    BossIdle,
    BossDead,
    BossMeleeHit,
    BossRangeHit,
    BossLaserCass,
    BossImmune,
    BossParry,
}
public class BossStateManager : MonoBehaviour
{
    public BossBaseState currentState;
    public BossIdleState IdleState = new BossIdleState();
    public BossAttackState AttackState = new BossAttackState();
    public BossImmuneState ImmuneState = new BossImmuneState();
    public BossDeadState DeadState = new BossDeadState();

     
    public HealthBossController HealthBossController;
    private string currentAnimationState = "BossIdle";
    public Animator bossAnim;

    // Start is called before the first frame update
    void Start()
    {
        currentState = IdleState;
        currentState.EnterState(this);
        bossAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public bool isStartFight()
    {
        if(HealthBossController != null)
        {
            if (HealthBossController.currentHealth != HealthBossController.maxHealth)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    public void SwitchState(BossBaseState state)
    {

        currentState = state; 
        state.EnterState(this);
    }
    public void ChangeAnimationState(string animationState)
    {
        if(currentAnimationState == animationState) { return; }

        bossAnim.Play(animationState);
        // bien chua thoi gian animation

        //Invoke

        currentAnimationState = animationState;
    }
    public void ChangeBossIdleState()
    {
        if (currentAnimationState == BossAnimation.BossIdle.ToString()) { return; }

        bossAnim.Play(BossAnimation.BossIdle.ToString());
        

        currentAnimationState = BossAnimation.BossIdle.ToString();
    }
    public void ChangeBossMeleeAttackState()
    {
        if (currentAnimationState == BossAnimation.BossMeleeHit.ToString()) { return; }

        bossAnim.Play(BossAnimation.BossMeleeHit.ToString());
        

        currentAnimationState = BossAnimation.BossMeleeHit.ToString();
    }
    public void ChangeBossRangeAttackState()
    {
        if (currentAnimationState == BossAnimation.BossRangeHit.ToString()) { return; }

        bossAnim.Play(BossAnimation.BossRangeHit.ToString());


        currentAnimationState = BossAnimation.BossRangeHit.ToString();
    }

    public void ChangeBossLaserAttackState()
    {
        if (currentAnimationState == BossAnimation.BossLaserCass.ToString()) { return; }

        bossAnim.Play(BossAnimation.BossLaserCass.ToString());


        currentAnimationState = BossAnimation.BossLaserCass.ToString();
    }

    public void ChangeBossImmuneState()
    {
        if (currentAnimationState == BossAnimation.BossImmune.ToString()) { return; }

        bossAnim.Play(BossAnimation.BossImmune.ToString());


        currentAnimationState = BossAnimation.BossImmune.ToString();
    }
    public void ChangeBossParryState()
    {
        if (currentAnimationState == BossAnimation.BossParry.ToString()) { return; }

        bossAnim.Play(BossAnimation.BossParry.ToString());


        currentAnimationState = BossAnimation.BossParry.ToString();
    }
    public void ChangeBossDeadState()
    {
        if (currentAnimationState == BossAnimation.BossDead.ToString()) { return; }

        bossAnim.Play(BossAnimation.BossDead.ToString());


        currentAnimationState = BossAnimation.BossDead.ToString();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentState != null)
        {
            currentState.OnCollisionEnter(this, collision);
        }
    }
}
