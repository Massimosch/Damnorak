using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-Random Wander", menuName = "Enemy Logic/Idle Logic/Random Wander")]

public class EnemyIdleRandomWander : EnemyIdleSOBase
{


    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        
    }


    public override void DoExitLogic()
    {
        base.DoExitLogic();

        
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();

    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }
}
