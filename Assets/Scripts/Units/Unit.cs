using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour, IDamageable, ITriggerCheckable {

    const float minPathUpdateTime = .2f;
    public float speed = 5f;
    public float turnSpeed = 3f;
    public float turnDst = 5f;
    public float stoppingDst = 10f;
    
    private GridA grid;
    private Path path;
    public Transform target;
    public Rigidbody rb;
    
    [SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }
    
    #region State Machine Variables
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyIdleState IdleState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }
    #endregion

    #region ScriptableObject Variables
    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }
    #endregion

    protected virtual void Awake() {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);
        
        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);
    }

    protected virtual void Start() {
        grid = GameObject.Find("A*").GetComponent<GridA>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(UpdatePath());
        CurrentHealth = MaxHealth;

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);
        StateMachine.Initialize(IdleState);
    }

    void Update() {
        StateMachine.CurrentEnemyState.FrameUpdate();
    }

    void FixedUpdate() {
        StateMachine.CurrentEnemyState.PhysicsUpdate();
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
        if (pathSuccessful) {
            path = new Path(waypoints, transform.position, turnDst, stoppingDst);
            StopCoroutine(nameof(FollowPath));
            StartCoroutine(nameof(FollowPath));
        }
    }

    IEnumerator UpdatePath() {
        if (Time.timeSinceLevelLoad < .3f) {
            yield return new WaitForSeconds(.3f);
        }
        
        while (true) {
            yield return new WaitForSeconds(minPathUpdateTime);
            Vector3 targetPos = target.position;
            PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
        }
    }

    public virtual void FollowPathAction(Path path, int pathIndex)
    {
        Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
    }

    IEnumerator FollowPath() {
        bool followingPath = true;
        int pathIndex = 0;
        //float speedPercent = 1;

        while (followingPath) {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D)) {
                if (pathIndex == path.finishLineIndex) {
                    followingPath = false;
                    break;
                } else {
                    pathIndex++;
                }
            }

            if (followingPath) {
                FollowPathAction(path, pathIndex);
            }
            yield return null;
        }
    }

    public void Damage(float damageAmount) {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0f) {
            Die();
        }
    }

    public void Die() {
        Destroy(gameObject);
    }

    public void OnDrawGizmos() {
        if (path != null) {
            path.DrawWithGizmos();
        }
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepSound
    }

    public void SetAggroStatus(bool isAggroed)
    {
        
    }

    public void SetStrikingDistanceBool(bool isWithinStrikingDistance)
    {
        
    }
}
