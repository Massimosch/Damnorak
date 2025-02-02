using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public void FinishAttacking()
    {
        PlayerSettings.State = "Idle";
        PlayerSettings.animator.Play("Idle");
    }

    public void Attack()
    {
        GameObject projectile = Instantiate(PlayerSettings.Attack, transform.position + transform.forward * 5, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().linearVelocity = transform.position + transform.forward * 5;
    }

    void Update()
    {
        if(Input.GetButton("Fire1") && PlayerSettings.State == "Idle")
        {
            PlayerSettings.State = "Attacking";

            string AttackNumber = Random.Range(1,5).ToString();
            PlayerSettings.animator.Play("Attack" + AttackNumber);
        }
    }
}
