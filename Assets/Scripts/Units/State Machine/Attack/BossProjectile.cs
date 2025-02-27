using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float Speed = 10f;
    public float Damage = 1f;

    [Header("Audio Clips")]
    public AudioClip fireSound; 
    public AudioClip hitSound;   

    private Vector3 moveDirection;
    private AudioSource audioSource;

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.pitch = 1.4f;

        if (fireSound != null)
        {
            audioSource.PlayOneShot(fireSound, 0.3f);
        }
    }

    void Update()
    {
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
