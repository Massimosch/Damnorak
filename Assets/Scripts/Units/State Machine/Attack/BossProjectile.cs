using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float Speed = 10f;
    public float Damage = 1f;
    public AudioClip hitSound; // Assign this in the inspector

    private Vector3 moveDirection;
    private AudioSource audioSource;

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    void Start()
    {
        // Add an AudioSource to the projectile if not already there
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = hitSound;
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
            PlaySoundAndDestroy();
        }
        Destroy(gameObject, 1.5f);
    }

    void PlaySoundAndDestroy()
    {
        if (hitSound != null)
        {
            GameObject soundObject = new GameObject("ProjectileHitSound");
            AudioSource tempAudio = soundObject.AddComponent<AudioSource>();
            tempAudio.clip = hitSound;
            tempAudio.Play();
            Destroy(soundObject, hitSound.length);
        }

        Destroy(gameObject);
    }
}
