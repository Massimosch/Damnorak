using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Melee-Hit", menuName = "Enemy Logic/Attack Logic/Melee Hit")]
public class EnemyAttackingMeleeHit : EnemyAttackSOBase
{
    private bool isAttacking;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        enemy.Animator.SetBool("Walking", false);
        isAttacking = true;

        Attack();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (!isAttacking) return;

        if (enemy.Target != null)
        {
            float distance = Vector3.Distance(enemy.transform.position, enemy.Target.position);

            if (distance > enemy.AttackDistance)
            {
                isAttacking = false;
                enemy.StateMachine.ChangeState(enemy.ChaseState);
            }
        }
    }

    private void Attack()
    {
            enemy.Animator.SetTrigger("Attack");
    }

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);

        if (triggerType == Enemy.AnimationTriggerType.Attack)
        {
            DealDamage();
            Attack();
        }
    }

    private void DealDamage()
    {
            Debug.Log("Enemy deals melee damage to player!");
            HealthScript.TakeDamage(1);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        isAttacking = false;
        enemy.Animator.SetBool("Walking", true);
    }
}
