using UnityEngine;

public class EnemyIdleSOBase : ScriptableObject
{
    protected Unit enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;

    public virtual void Initialize(GameObject gameObject, Unit enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void DoEnterLogic()
    {

    }

    public virtual void DoExitLogic()
    {
        //ResetValues();
    }

    public virtual void DoFrameUpdateLogic()
    {
        if (enemy.IsAggroed)
        {
            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }
    }

    public virtual void DoPhysicsLogic()
    {

    }

    public virtual void DoAnimationTriggerEventLogic(Unit.AnimationTriggerType triggerType)
    {

    }

    public virtual void ResetValues()
    {

    }
}
