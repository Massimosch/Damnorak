using UnityEngine;

public class EnemyAttackSOBase : ScriptableObject
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
