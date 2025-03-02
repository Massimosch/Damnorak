using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AudioClip explosionSound;
    private AudioSource audioSource;
    public Enemy enemy;
    public int Damage = 10;

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

        if (collision.collider.CompareTag("Enemy"))
        {
            IDamageable damageable = collision.collider.GetComponent<IDamageable>();
            damageable.Damage(Damage);
        }
        Destroy(gameObject, 0.6f);
    }
}
