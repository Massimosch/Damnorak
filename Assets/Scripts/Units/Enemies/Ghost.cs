using UnityEngine;

public class Ghost : Unit
{
    
    void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

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
