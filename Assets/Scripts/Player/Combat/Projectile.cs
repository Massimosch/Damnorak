using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AudioClip explosionSound;
    private AudioSource audioSource;
    public Enemy enemy;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        GetComponent<Collider>().enabled = false;
        audioSource.PlayOneShot(explosionSound);
        Destroy(Instantiate(PlayerSettings.AttackExplosion, transform.position, Quaternion.identity), 3);

        if(collision.collider.name == "DrawX")
        {
            Destroy(Instantiate(LevelSettings.SecretRoomExplosion, collision.collider.transform.position, Quaternion.identity), 4f);
            Destroy(collision.gameObject);
        }

        Debug.Log($"Projectile hit: {collision.collider.name} with tag: {collision.collider.tag}");

        if (collision.collider.CompareTag("Enemy"))
        {
            IDamageable damageable = collision.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                Debug.Log($"Found IDamageable on {collision.collider.name}");
                damageable.Damage(10f);
            }
            else
            {
                Debug.LogWarning($"No IDamageable found on {collision.collider.name}");
            }
        }

        Destroy(gameObject, 0.6f);
    }
}
