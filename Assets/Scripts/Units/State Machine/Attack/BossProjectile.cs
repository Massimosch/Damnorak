using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float Speed = 10f;
    public float Damage = 1f;
    private Vector3 moveDirection;

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    void Update()
    {
        // Move in the aimed direction
        transform.position += moveDirection * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthScript.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}
