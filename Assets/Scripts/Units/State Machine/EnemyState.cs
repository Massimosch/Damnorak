using UnityEngine;

public class EnemyState
{
    protected Unit enemy;
    protected EnemyStateMachine enemyStateMachine;

    public EnemyState(Unit enemy, EnemyStateMachine enemyStateMachine)
    {
        this.enemy = enemy;
        this.enemyStateMachine = enemyStateMachine;
    }

    public virtual void EnterState() {}
    public virtual void ExitState() {}
    public virtual void FrameUpdate() {}
    public virtual void PhysicsUpdate() {}
    public virtual void AnimationTriggerEvent(Unit.AnimationTriggerType triggerType) {}
}
