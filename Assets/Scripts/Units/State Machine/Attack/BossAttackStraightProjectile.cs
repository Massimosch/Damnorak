using UnityEngine;

[CreateAssetMenu(fileName = "BossAttack-StraightProjectile", menuName = "Enemy Logic/Boss Attack Logic/Straight Projectile")]
public class BossAttackStraightProjectile : EnemyAttackSOBase
{
    [SerializeField] private GameObject projectilePrefab;
    private Transform firePoint;
    [SerializeField] private float fireRate = 1.5f;
    private float nextFireTime;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        if (triggerType == Enemy.AnimationTriggerType.Attack)
        {
            FireProjectile();
        }
    }


    public override void DoFrameUpdateLogic()
    {
        if (enemy == null || enemy.Target == null)
            return;

        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.Target.position);

        if (distanceToPlayer > enemy.AttackDistance)
        {
            Debug.Log("Player out of range. Switching back to Chase.");
            enemy.StateMachine.ChangeState(enemy.ChaseState);
            return;
        }

        // Rotate the boss to face the player
        Vector3 targetDirection = (enemy.Target.position - enemy.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(targetDirection);
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);

        // Trigger attack animation if ready to fire
        if (Time.time >= nextFireTime)
        {
            enemy.Animator.SetTrigger("Attack"); // Ensure you have an "Attack" trigger in the Animator
            nextFireTime = Time.time + 1f / fireRate;
        }
    }



    private void FireProjectile()
    {
        if (projectilePrefab != null && firePoint != null && enemy.Target != null)
        {
            // Aim at the player's current position
            Vector3 direction = (enemy.Target.position - firePoint.position).normalized;

            // Instantiate and rotate toward player
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
            Debug.Log("Boss fired a projectile!");

            // Pass direction to the projectile for movement
            BossProjectile projectileScript = projectile.GetComponent<BossProjectile>();
            if (projectileScript != null)
            {
                projectileScript.SetDirection(direction);
            }
        }
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
        firePoint = gameObject.transform.Find("FirePoint");

        if (firePoint == null)
        {
            Debug.LogError("Fire Point not found in boss prefab.");
        }
    }
}
