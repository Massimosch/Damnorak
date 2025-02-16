using UnityEngine;

public class EnemyChaseState : EnemyState
{

    public EnemyChaseState(Unit enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.EnemyChaseBaseInstance.DoPhysicsLogic();
    }

    public override void EnterState()
    {
        base.EnterState();
        enemy.EnemyChaseBaseInstance.DoEnterLogic();

    }
    
    public override void ExitState()
    {
        base.ExitState();
        enemy.EnemyChaseBaseInstance.DoExitLogic();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        enemy.EnemyChaseBaseInstance.DoFrameUpdateLogic();
    }

    public override void AnimationTriggerEvent(Unit.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
        enemy.EnemyChaseBaseInstance.DoAnimationTriggerEventLogic(triggerType);
    }
}
