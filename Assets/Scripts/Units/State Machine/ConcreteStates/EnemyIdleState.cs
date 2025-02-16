using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Vector3 _targetPos;
    private Vector3 _direction;

    public EnemyIdleState(Unit enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Unit.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        _targetPos = GetRandomPointInCircle();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _direction = (_targetPos - enemy.transform.position).normalized;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private Vector3 GetRandomPointInCircle()
    {
        Vector2 randomCircle = Random.insideUnitCircle * 5f; // 5 yksikön säde
        Vector3 randomPoint = new(enemy.transform.position.x + randomCircle.x, enemy.transform.position.y, enemy.transform.position.z + randomCircle.y);
        return randomPoint;
    }

}
