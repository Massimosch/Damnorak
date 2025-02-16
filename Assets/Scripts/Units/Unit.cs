using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Linq;

public class Unit : MonoBehaviour, IDamageable, ITriggerCheckable {

	const float minPathUpdateTime = .2f;
	public float speed = 20;
	public float turnSpeed = 3;
	public float turnDst = 5;
	public float stoppingDst = 10;

	Path path;
	public Transform target;

    [SerializeField] public float MaxHealth {get; set;} = 100f;
    public float CurrentHealth {get; set;}

	#region State Machine Variables
	public EnemyStateMachine StateMachine {get; set;}
	public EnemyIdleState IdleState {get; set;}
	public EnemyChaseState ChaseState {get; set;}
	public EnemyAttackState AttackState {get; set;}
    public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }
    #endregion

	#region ScriptableObject Variables
	[SerializeField] private EnemyIdleSOBase EnemyIdleBase;
	[SerializeField] private EnemyChaseSOBase EnemyChaseBase;
	[SerializeField] private EnemyAttackSOBase EnemyAttackBase;

	public EnemyIdleSOBase EnemyIdleBaseInstance {get; set;}
	public EnemyChaseSOBase EnemyChaseBaseInstance {get; set;}
	public EnemyAttackSOBase EnemyAttackBaseInstance {get; set;}
	#endregion

	protected virtual void Awake()
	{
		EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
		EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
		EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);

		StateMachine = new EnemyStateMachine();
		IdleState = new EnemyIdleState(this, StateMachine);
		ChaseState = new EnemyChaseState(this, StateMachine);
		AttackState = new EnemyAttackState(this, StateMachine);
	}

	
	protected virtual void Start() 
	{
		StartCoroutine (UpdatePath ());
		CurrentHealth = MaxHealth;

		EnemyIdleBaseInstance.Initialize(gameObject, this);
		EnemyChaseBaseInstance.Initialize(gameObject, this);
		EnemyAttackBaseInstance.Initialize(gameObject, this);

		StateMachine.Initialize(IdleState);
	}

    void Update()
    {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    void FixedUpdate()
    {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
		if (pathSuccessful) {
			path = new Path(waypoints, transform.position, turnDst, stoppingDst);

			StopCoroutine(nameof(FollowPath));
			StartCoroutine(nameof(FollowPath));
		}
	}

	public IEnumerator UpdatePath() 
	{
		if (Time.timeSinceLevelLoad < .3f) 
		{
			yield return new WaitForSeconds(.3f);
		}

		while (true) 
		{
			yield return new WaitForSeconds(minPathUpdateTime);
			Vector3 targetPos = target.position;
			PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
		}
	}

	IEnumerator FollowPath() 
	{
		bool followingPath = true;
		int pathIndex = 0;

		transform.LookAt(path.lookPoints[0]);

		float speedPercent = 1;

		while (followingPath) 
		{
			Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);

			while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D)) 
			{
				if (pathIndex == path.finishLineIndex) 
				{
					followingPath = false;
					break;
				} 
				else 
				{
					pathIndex++;
				}
			}

			if (followingPath) 
			{
				if (pathIndex >= path.slowDownIndex && stoppingDst > 0) 
				{
					speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
					if (speedPercent < 0.01f) 
					{
						followingPath = false;
					}
				}

				Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
			}

			yield return null;
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			path.DrawWithGizmos ();
		}
	}

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;

		if (CurrentHealth <= 0f)
		{
			Die();
		}
    }

    public void Die()
    {
		Destroy(gameObject);
    }

	#region Distance Checks

	public void SetAggroStatus(bool isAggroed)
	{
		IsAggroed = isAggroed;
	}

	public void SetStrikingDistanceBool(bool isWithinStrikingDistance)
	{
		IsWithinStrikingDistance = isWithinStrikingDistance;
	}

	#endregion

	private void AnimationTriggerEvent(AnimationTriggerType triggerType)
	{
		StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
	}

    public enum AnimationTriggerType
	{
		EnemyDamaged,
		PlayFootstepSound
	}
}