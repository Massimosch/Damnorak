using UnityEngine;

[CreateAssetMenu(fileName = "BossAttack-StraightProjectile", menuName = "Enemy Logic/Boss Attack Logic/Straight Projectile")]
public class BossAttackStraightProjectile : EnemyAttackSOBase
{
    [SerializeField] private GameObject projectilePrefab;
    private Transform firePoint;
    [SerializeField] private float fireRate = 1.5f;  // Adjusted for balance
    private float nextFireTime;

    public override void DoFrameUpdateLogic()
    {
        if (enemy == null || enemy.Target == null)
            return;

        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.Target.position);

        if (distanceToPlayer > enemy.AttackDistance)
        {
            // Exit attack state and return to chase if player moves away
            Debug.Log("Player out of range. Switching back to Chase.");
            enemy.StateMachine.ChangeState(enemy.ChaseState);
            return;
        }

        // Fire projectiles while in range
        if (Time.time >= nextFireTime)
        {
            FireProjectile();
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
