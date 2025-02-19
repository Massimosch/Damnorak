using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool isAttacking = false;
    private bool canAttack = true;
    public float projectileSpeed = 10f;
    public float projectileSpawnHeight = 1.5f;


    void Update()
    {
        if (Input.GetButton("Fire1") && PlayerSettings.State == "Idle" && canAttack)
        {
            StartAttack();
        }

        if (isAttacking)
        {          
            AnimatorStateInfo stateInfo = PlayerSettings.animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1f)
            {
                FinishAttacking();
            }
        }
    }

    // Hyökkäyksen aloitus
    private void StartAttack()
    {
        PlayerSettings.State = "Attacking";
        isAttacking = true;
        canAttack = false; 
        string attackNumber = Random.Range(1, 5).ToString(); 
        PlayerSettings.animator.Play("Attack" + attackNumber);
    }


    // Hyökkäyksen lopetus
    private void FinishAttacking()
    {
        isAttacking = false;
        PlayerSettings.State = "Idle";
        PlayerSettings.animator.Play("Idle");
        canAttack = true;
    }

    // Hyökkäys
    public void Attack()
    {
        GameObject projectile = Instantiate(PlayerSettings.Attack, transform.position + transform.forward * 3 + transform.up * projectileSpawnHeight, Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 attackDirection = transform.forward * projectileSpeed;

        rb.linearVelocity = attackDirection;

        projectile.transform.rotation = Quaternion.LookRotation(attackDirection);

        Destroy(projectile, 2.5f);
    }
}