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

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (m_Enemy.Target == null)
            return;

        m_Distance = Vector3.Distance(m_Agent.transform.position, m_Enemy.Target.position);
        bool isWalking = m_Agent.velocity.magnitude > 0.1f;
        m_Animator.SetBool("Walking", isWalking);

        // Switch to Attack State when within attack distance
        if (m_Distance <= m_Enemy.AttackDistance)
        {
            m_Agent.isStopped = true;
            m_Enemy.StateMachine.ChangeState(m_Enemy.AttackState);
            Debug.Log("Switching to Attack State.");
        }
        else
        {
            m_Agent.isStopped = false;
            m_Agent.destination = m_Enemy.Target.position;
        }
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
        m_Enemy = enemy;
        m_Agent = gameObject.GetComponent<NavMeshAgent>();
        m_Animator = gameObject.GetComponent<Animator>();
    }
}
