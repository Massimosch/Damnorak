using UnityEngine;

public class Ghost : Unit
{
    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetAggroStatus(true);
            StateMachine.ChangeState(ChaseState);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetAggroStatus(false);
            StateMachine.ChangeState(IdleState);
        }
    }
}
