using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy Logic/Chase Logic/Direct Chase")]
public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
{

    public override void DoAnimationTriggerEventLogic(Unit.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        
        Debug.Log("Hello from Chase");
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

        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - enemy.transform.position).normalized;
            enemy.transform.position += direction * 3f * Time.deltaTime; // Muokkaa nopeutta tarpeen mukaan
        }
    }


    public override void ResetValues()
    {
        base.ResetValues();
    }

    public override void Initialize(GameObject gameObject, Unit enemy)
    {
        base.Initialize(gameObject, enemy);
    }
}
