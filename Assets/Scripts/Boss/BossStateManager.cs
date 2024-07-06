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
    Animator bossAnim;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentState != null)
        {
            currentState.OnCollisionEnter(this, collision);
        }
    }
}
