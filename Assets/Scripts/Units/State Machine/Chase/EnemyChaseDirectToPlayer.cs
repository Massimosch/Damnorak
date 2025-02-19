using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Chase-Direct Chase", menuName = "Enemy Logic/Chase Logic/Direct Chase")]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyChaseDirectToPlayer : EnemyChaseSOBase
{
    private NavMeshAgent m_Agent;
    private float m_Distance;
    private Animator m_Animator;
    private Enemy m_Enemy;
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
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

        m_Distance = Vector3.Distance(m_Agent.transform.position, m_Enemy.Target.position);

        bool isWalking = m_Agent.velocity.magnitude > 0.1f;
        m_Animator.SetBool("Walking", isWalking);

        if (m_Distance < m_Enemy.AttackDistance)
        {
            m_Agent.isStopped = true;
            //m_Animator.SetBool("Attack", true);
            HealthScript.TakeDamage(1f);
        }
        else
        {
            m_Agent.isStopped = false;
            //m_Animator.SetBool("Attack", false);
            m_Agent.destination = m_Enemy.Target.position;
        }
    }


    void OnAnimatorMove()
    {
        if(m_Animator.GetBool("Attack") == false)
        {
            m_Agent.speed = (m_Animator.deltaPosition / Time.deltaTime).magnitude;
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }


    public override void ResetValues()
    {
        base.ResetValues();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);

        m_Enemy = enemy;
        m_Agent = gameObject.GetComponent<NavMeshAgent>();
        m_Animator = gameObject.GetComponent<Animator>();
    }
}
