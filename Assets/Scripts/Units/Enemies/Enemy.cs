using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMoveable, ITriggerCheckable
{
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }

    public Transform Target;
    public float AttackDistance;
    private SkinnedMeshRenderer enemyRenderer;
    private Color originalColor;
    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private float flashDuration = 0.2f;

    #region StateMachine Variables
    public EnemyIdleState IdleState { get; set;}
    public EnemyStateMachine StateMachine { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }
    #endregion

    #region Scriptable Object Variables
    [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [SerializeField] private EnemyAttackSOBase EnemyAttackBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }
    #endregion

    void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);

        StateMachine = new EnemyStateMachine();
        IdleState = new EnemyIdleState(this, StateMachine);
        ChaseState = new EnemyChaseState(this, StateMachine);
        AttackState = new EnemyAttackState(this, StateMachine);

        enemyRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        originalColor = enemyRenderer.material.color;
    }

    void Start()
    {
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

    public void Damage(float damageAmount)
    {
        Debug.Log($"Before Damage: {CurrentHealth}");
        CurrentHealth -= damageAmount;
        Debug.Log($"After Damage: {CurrentHealth}");

        StartCoroutine(nameof(FlashRed));
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }


    private IEnumerator FlashRed()
    {
        if (enemyRenderer == null)
        {
            Debug.LogError("enemyRenderer is null!");
            yield break;
        }

        // Get the material property
        Material material = enemyRenderer.material;

        // Store original emission color
        Color originalEmission = material.GetColor("_EmissionColor");

        // Set emission to bright red
        material.SetColor("_EmissionColor", Color.red);
        material.EnableKeyword("_EMISSION");

        // Flash duration
        yield return new WaitForSeconds(0.2f);

        // Reset to original emission color
        material.SetColor("_EmissionColor", originalEmission);
    }




    public void Die()
    {
        Destroy(gameObject);
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggetType)
    {
        StateMachine.CurrentEnemyState.AnimationTriggerEvent(triggetType);
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

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        PlayFootstepsounds
    }
}
