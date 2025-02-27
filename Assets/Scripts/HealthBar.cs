using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image _image;
    private Camera _camera;

    void Start()
    {
        _image = GetComponent<Image>();
        _camera = Camera.main;
    }

    void Update()
    {
        if (_camera != null)
        {
            transform.LookAt(transform.position + _camera.transform.forward);
        }
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        _image.fillAmount = currentHealth / maxHealth;
    }
}
