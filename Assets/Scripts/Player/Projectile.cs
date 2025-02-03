using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Destroy(Instantiate(PlayerSettings.AttackExplosion, transform.position, Quaternion.identity), 3);
        Destroy(gameObject);
    }
}
