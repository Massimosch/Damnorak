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

    override public void FollowPathAction(Path path, int pathIndex)
    {
        Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
        Quaternion newTR = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, newTR, Time.deltaTime * turnSpeed);

        Vector3 translation = speed * Time.deltaTime * transform.forward;
        rb.MovePosition(rb.position + translation);
        rb.linearVelocity = Vector3.zero;
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
