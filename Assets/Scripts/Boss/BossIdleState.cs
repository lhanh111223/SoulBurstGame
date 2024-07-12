using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Boss
{
    public class BossIdleState : BossBaseState
    {

        public override void EnterState(BossStateManager boss)
        {
            Debug.Log("Go to Idle State!");
            boss.ChangeAnimationState(BossAnimation.BossIdle.ToString());
        }

        public override void OnCollisionEnter(BossStateManager boss, Collision2D collider)
        {
            //throw new NotImplementedException();
        }

        public override void UpdateState(BossStateManager boss)
        {
            if (boss.isStartFight())
            {
                Debug.Log("Start Attack");
                boss.SwitchState(boss.AttackState);
            }
        }
    }
}
