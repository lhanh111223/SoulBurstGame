
using UnityEngine;

public abstract class BossBaseState 
{
    public abstract void EnterState(BossStateManager boss);

    public abstract void UpdateState(BossStateManager boss);

    public abstract void OnCollisionEnter(BossStateManager boss, Collision2D collider);
}
