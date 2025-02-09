using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AudioClip explosionSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        GetComponent<Collider>().enabled = false;
        audioSource.PlayOneShot(explosionSound);
        Destroy(Instantiate(PlayerSettings.AttackExplosion, transform.position, Quaternion.identity), 3);

        Destroy(gameObject, 0.6f);
    }
}
