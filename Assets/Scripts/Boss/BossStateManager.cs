using Assets.Scripts.Boss;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [Header("State")]

    public BossBaseState currentState;
    public BossIdleState IdleState = new BossIdleState();
    public BossAttackState AttackState = new BossAttackState();
    public BossImmuneState ImmuneState = new BossImmuneState();
    public BossDeadState DeadState = new BossDeadState();
    [Header("HealthBoss")]
    public HealthBossController HealthBossController;
    public GameObject HealthBarBoss;
    private string currentAnimationState = "BossIdle";
    [Header("Animation")]
    public Animator bossAnim;

    [Header("Spawner")]
    public Transform startPosition;
    public List<Transform> spawnPosition;
    public GameObject[] enemyInstances;
    public GameObject meleeEnemy;
    public GameObject rangeEnemy;
    public bool isImmune;
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
    public void loadSceneWinGame()
    {
        Debug.Log("Victory");
        SceneManager.LoadScene("WinGameScene");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentState != null)
        {
            currentState.OnCollisionEnter(this, collision);
        }
        if (collision.gameObject.CompareTag("Bullet1") && currentState.GetType() != typeof(BossImmuneState))
        {
            HealthBossController.takeDamage(10);
            collision.gameObject.SetActive(false);
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
